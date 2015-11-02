$(function () {
    isCreate = true;
    //on submitting form
    $("#GoalCreateForm").submit(function (ev) {
        ev.preventDefault();
        if (isCreate)
            postUrl = "/Home/CreateGoal";
        else
            postUrl = "/Home/UpdateGoal";

        if ($("#GoalCreateForm").valid()) {
            var GoalData = {};
            if (!isCreate)
                GoalData.ID = updateGoalId;
            GoalData.Title = $("#GoalTitle").val();
            GoalData.Description = $("#GoalDescription").val();
            GoalData.Year = $("#GoalYear option:selected").text();
            GoalData.Quarter = $("#GoalQuarter option:selected").text();
            GoalData.UnitOfMeasurement = $("#GoalUnit").val();
            GoalData.MeasurementValue = $("#GoalUnitValue").val();
            GoalData.IsHigher = $("#IsHigherValue").is(":checked");

            if ($("#RuleListTable").find("tr").length > 0) {
                //adding goal rules
                var GoalRules = Array();
                $("#RuleListTable").find("tr").each(function () {
                    var rule = {};
                    rule.RangeFrom = $(this).find(".RangeFrom").text();
                    rule.RangeTo = $(this).find(".RangeTo").text();
                    rule.Rating = $(this).find(".Rating").text();
                    GoalRules.push(rule);
                });
                GoalData.GoalRules = GoalRules;

                //submitting data
                $.ajax({
                    url: postUrl,
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify({ GoalData: GoalData }),
                    beforeSend: showLoading(),
                    success: function (dt) {
                        hideLoading();
                        alert(dt.message);
                        if (dt.success) {
                            location.reload(true);
                        }
                    },
                    error: function (dt) {
                        hideLoading();
                        console.log(dt);
                    }
                });
            }
            else {
                alert("Please define atleast one rule!");
                $("#collapse3").collapse('show');
            }
            console.log(JSON.stringify(GoalData));

        }
    });

    //resetting form
    $("#resetButton").click(function () {
        isCreate = true;
        $("#submitButton").attr("disabled", false);
        $('#RuleListTable').html("");
    });
    hideLoading = function () { $("#loading").hide(); }
    showLoading = function () { $("#loading").show(); }
    ViewGoal = function (id) {
        $("#submitButton").attr("disabled", true);
        //alert("asdf"+id);
        $.ajax({
            url: "/Home/GetGoalById",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({ Id: id }),
            beforeSend: showLoading(),
            success: function (dt) {
                hideLoading();
                //console.log(dt);
                var data = JSON.parse(dt);
                var goal = data.goal;
                var quarter = data.quarter;
                var rules = data.rules;

                //goal details filling
                $("#GoalTitle").val(goal.GoalTitle);
                $("#GoalDescription").val(goal.GoalDescription);
                $("#GoalYear option:selected").text(quarter.YEAR);
                $("#GoalQuarter option:selected").text(quarter.Quater);
                $("#GoalUnit").val(goal.UnitOfMeasurement);
                $("#GoalUnitValue").val(goal.MeasurementValue);
                if (goal.IsHigherValueGood)
                    $("#IsHigherValue").attr("checked", true);
                else
                    $("#IsHigherValue").attr("checked", false);

                //rules filling
                $('#RuleListTable').html("");
                $(rules).each(function () {
                    //console.log(this);
                    $('#RuleListTable').append('<tr id="rule' + this.Goal_RuleID + '"><td class="col-md-3 RangeFrom">' + this.Performance_RangeFrom + '</td><td class="col-md-3 RangeTo">' + this.Performance_RangeTo + '</td><td class="col-md-3 Rating">' + this.Rating + '</td><td class="col-md-1"><span id="Edit" onclick="EditGoalRule(rule' + this.Goal_RuleID + ')" class="glyphicon glyphicon-pencil"/>&nbsp;<span onclick="RemoveGoalRule(rule' + this.Goal_RuleID + ')" class="glyphicon glyphicon-remove" /></td></tr>');
                });
                $("#collapse1").collapse('hide');
                $("#collapse2").collapse('show');
            },
            error: function (dt) {
                hideLoading();
                console.log(dt);
            }
        });
    }

    //edit goal function
    EditGoal = function (id) {
        //alert(id);
        ViewGoal(id);
        $("#submitButton").attr("disabled", false);
        isCreate = false;
        updateGoalId = id;
    }

    //delete goal
    DeleteGoal = function (id) {
        if (confirm("Are you sure to delete?")) {
            $.ajax({
                url: "/Home/DeleteGoal",
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify({ Id: id }),
                beforeSend: showLoading(),
                success: function (dt) {
                    hideLoading();
                    alert(dt.message);
                    if (dt.success) {
                        location.reload(true);
                    }
                },
                error: function (dt) {
                    hideLoading();
                    console.log(dt);
                }
            });
        }
    }

    //form validation
    $("#GoalCreateForm").validate({
        rules: {
            GoalTitle: {
                minlength: 3,
                required: true
            },
            GoalDescription: {
                minlength: 15,
                required: true
            },
            GoalYear: {
                required: true
            },
            GoalQuarter: {
                required: true
            },
            GoalUnit: {
                minlength: 3,
                required: true
            },
            GoalUnitValue: {
                required: true
            }
        },
        highlight: function (element) {
            $(element).closest('.form-group').addClass('has-error');
        },
        unhighlight: function (element) {
            $(element).closest('.form-group').removeClass('has-error');
        },
        errorElement: 'span',
        errorClass: 'help-block',
        errorPlacement: function (error, element) {
            if (element.parent('.input-group').length) {
                error.insertAfter(element.parent());
            } else {
                error.insertAfter(element);
            }
        }
    });
    //Add the Row of Goal Rule to the list
    var count = 0;
    AddGoalRule = function () {
        if (count > -1) {
            count += 1;
            var RangeFrom = $("#RangeFrom").val();
            var RangeTo = $("#RangeTo").val();
            var Rating = $("#Rating").val();
            $('#RuleListTable').append('<tr id="rule' + count + '"><td class="col-md-3 RangeFrom">' + RangeFrom + '</td><td class="col-md-3 RangeTo">' + RangeTo + '</td><td class="col-md-3 Rating">' + Rating + '</td><td class="col-md-1"><span id="Edit" onclick="EditGoalRule(rule' + count + ')" class="glyphicon glyphicon-pencil"/>&nbsp;<span onclick="RemoveGoalRule(rule' + count + ')" class="glyphicon glyphicon-remove" /></td></tr>');
            $("#RangeFrom").val(null);
            $("#RangeTo").val(null);
            $("#Rating").val(null);
        }
    }
    EditGoalRule = function (id) {
        var RangeFrom = $(id).find("td:nth-child(1)").html();
        var RangeTo = $(id).find("td:nth-child(2)").html()
        var Rating = $(id).find("td:nth-child(3)").html();
        $("#RangeFrom").val(RangeFrom);
        $("#RangeTo").val(RangeTo);
        $("#Rating").val(Rating);
        $(id).remove();
    }
    RemoveGoalRule = function (id) {
        $(id).remove();
    }
});