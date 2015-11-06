$(function () {
    isCreate = true;
    $('[data-toggle="tooltip"]').tooltip();

    //form validation
    var validator = $("#GoalCreateForm").validate({
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

    //reset form and validations
    ResetForm = function () {
        validator.resetForm();
        $('.form-group').removeClass('has-error');
    }
    //on submitting form
    $("#GoalCreateForm").submit(function (ev) {
        ev.preventDefault();
        if (isCreate)
            postUrl = "/Home/CreateGoal";
        else {
            postUrl = "/Home/UpdateGoal";
        }
        if ($("#GoalCreateForm").valid()) {
            var GoalData = {};
            if (!isCreate) GoalData.ID = updateGoalId;
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
                        bootbox.alert(dt.message, function () {
                            if (dt.success) {
                                location.reload(true);
                            }
                        });
                    },
                    error: function (dt) {
                        hideLoading();
                        if (dt.readyState == 0) {
                            bootbox.alert("Please check your internet connection!");
                        }
                        console.log(dt);
                    }
                });
            }
            else {
                bootbox.alert("Please define atleast one rule!", function () {
                    $("#collapse3").collapse('show');
                });
            }
            console.log(JSON.stringify(GoalData));
        }
    });

    //resetting form
    $("#resetButton").click(function () {
        isCreate = true;
        ResetForm();
        $("#submitButton").attr("disabled", false);
        $('#RuleListTable').html("");
    });

    //loading overlay events
    hideLoading = function () { $("#loading").hide(); }
    showLoading = function () { $("#loading").show(); }

    //goal view
    ViewGoal = function (id) {
        ResetForm();
        $("#submitButton").attr("disabled", true);
        //alert("asdf"+id);
        $.ajax({
            url: "/Home/GetGoalById",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({ Id: id }),
            beforeSend: showLoading(),
            success: function (data) {
                hideLoading();
                if (data.success) {
                    //console.log(data);
                    var goal = data.goal;
                    var quarter = data.quarter;
                    var rules = data.rules;

                    //goal details filling
                    $("#GoalTitle").val(goal.GoalTitle);
                    $("#GoalDescription").val(goal.GoalDescription);
                    $("#GoalYear option:selected").text(quarter.QuarterYear);
                    $("#GoalQuarter option:selected").text(quarter.GoalQuarter);
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
                }
            },
            error: function (dt) {
                hideLoading();
                if (dt.readyState == 0) {
                    bootbox.alert("Please check your interner connection!");
                }
                console.log(dt);
            }
        });
    }

    //edit goal function
    EditGoal = function (id) {
        //alert(id);
        isCreate = false;
        updateGoalId = id;
        //ViewGoal(id);
        $("#submitButton").attr("disabled", false);
        $.ajax({
            url: "/Home/GetGoalById",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({ Id: id }),
            beforeSend: showLoading(),
            success: function (data) {
                hideLoading();
                //console.log(data);
                if (data.success) {
                    //console.log(data.quarterList);
                    $("#GoalYear").html("");
                    var years = [];
                    quarterList = data.quarterList;
                    $.each(quarterList, function (i, v) {
                        if ($.inArray(v.QuarterYear, years) === -1) {
                            years.push(v.QuarterYear);
                            $("#GoalYear").append($("<option>", { value: v.QuarterYear, text: v.QuarterYear }));
                        }
                    });
                    var goal = data.goal;
                    var quarter = data.quarter;
                    var rules = data.rules;
                    $("#GoalQuarter").html("");
                    $.each(quarterList, function (i, v) {
                        if (v.QuarterYear == quarter.QuarterYear) {
                            $("#GoalQuarter").append($("<option>", { value: v.GoalQuarter, text: v.GoalQuarter }));
                        }
                    });
                    //goal details filling
                    $("#GoalTitle").val(goal.GoalTitle);
                    $("#GoalDescription").val(goal.GoalDescription);
                    $("#GoalYear").val(quarter.QuarterYear);
                    $("#GoalQuarter").val(quarter.GoalQuarter);
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
                    return true;
                }
            },
            error: function (dt) {
                hideLoading();
                if (dt.readyState == 0) {
                    bootbox.alert("Please check your interner connection!");
                }
                console.log(dt);
            }
        });
        $("#collapse1").collapse('hide');
        $("#collapse2").collapse('show');
    }

    //delete goal
    DeleteGoal = function (id) {
        bootbox.confirm("Are you sure to delete?", function (r) {
            if (r) {
                $.ajax({
                    url: "/Home/DeleteGoal",
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json",
                    data: JSON.stringify({ Id: id }),
                    beforeSend: showLoading(),
                    success: function (dt) {
                        hideLoading();
                        bootbox.alert(dt.message, function () {
                            if (dt.success) {
                                location.reload(true);
                            }
                        });
                    },
                    error: function (dt) {
                        hideLoading();
                        if (dt.readyState == 0) {
                            bootbox.alert("Please check your interner connection!");
                        }
                        console.log(dt);
                    }
                });
            }
        });
    }



    //Add the Row of Goal Rule to the list
    var count = 0;
    AddGoalRule = function () {
        if (count > -1) {
            count += 1;
            var RangeFrom = $("#RangeFrom").val();
            var RangeTo = $("#RangeTo").val();
            var Rating = $("#Rating").val();
            $('#RuleListTable').append('<tr id="rule' + count + '"><td class="col-md-3 RangeFrom">' + RangeFrom + '</td><td class="col-md-3 RangeTo">' + RangeTo + '</td><td class="col-md-3 Rating">' + Rating + '</td><td class="col-md-1"><span id="Edit" data-toggle="tooltip" data-placement="bottom" title="Edit Rule" onclick="EditGoalRule(rule' + count + ')" class="glyphicon glyphicon-pencil"/>&nbsp;<span class="glyphicon glyphicon-remove" data-toggle="tooltip" data-placement="bottom" title="Delete Rule" onclick="RemoveGoalRule(rule' + count + ')" /></td></tr>');
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

    //Goal Quater region

    //Add Quarter
    $("#GoalQuarterForm").submit(function (ev) {
        ev.preventDefault();
        if ($("#GoalQuarterForm").valid()) {
            var QuarterData = {};
            QuarterData.GoalQuarter = $("#GoalQuarters").val();
            QuarterData.QuarterYear = $("#GoalYears").val();
            QuarterData.GoalCreateFrom = $("#GoalCreateFrom").val();
            QuarterData.GoalCreateTo = $("#GoalCreateTo").val();

            $.ajax({
                url: "/Home/AddQuarter",
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify({ QuarterData: QuarterData }),
                beforeSend: showLoading(),
                success: function (dt) {
                    hideLoading();
                    bootbox.alert(dt.message, function () {
                        if (dt.success) {
                            location.reload(true);
                        }
                    });
                },
                error: function (dt) {
                    hideLoading();
                    if (dt.readyState == 0) {
                        bootbox.alert("Please check your interner connection!");
                    }
                    console.log(dt);
                }
            });
            console.log(JSON.stringify(QuarterData));
        }
    });

    //Quarter validation
    var validationQuarter = $("#GoalQuarterForm").validate({
        rules: {
            GoalQuarters: {
                required: true,
                range: [1, 4]
            },
            GoalYears: {
                minlength: 4,
                required: true
            },
            GoalCreateFrom: {
                required: true
            },
            GoalCreateTo: {
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
    $("#GoalYear").change(function () {
        var selection = this.value;
        $("#GoalQuarter").html("");
        $.each(quarterList, function (i, v) {
            if (v.QuarterYear == selection) {
                $("#GoalQuarter").append($("<option>", { value: v.GoalQuarter, text: v.GoalQuarter }));
            }
        });
    });

    //selection of goal
    $("#SelectAll").click(function () {
        if (this.checked)
            $("#DeleteMultiGoals").show();
        else
            $("#DeleteMultiGoals").hide();
        $('.SelectedGoal').prop('checked', this.checked);
    });
    $('.SelectedGoal').change(function () {
        if ($('.SelectedGoal:checked').length > 0)
            $("#DeleteMultiGoals").show();
        else
            $("#DeleteMultiGoals").hide();
    });
    $("#DeleteMultiGoals").click(function () {
        bootbox.confirm("Are you sure to delete selected goal(s)?", function (result) {
            if (result) {
                var ids = [];
                $('.SelectedGoal:checked').each(function () {
                    ids.push(this.value);
                });
                if (ids.length > 0) {
                    $.ajax({
                        url: "/Home/DeleteGoal",
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json",
                        data: JSON.stringify({ Id: ids }),
                        beforeSend: showLoading(),
                        success: function (data) {
                            hideLoading();
                            if (data.success) {
                                alert(data.message);
                                location.reload(true);
                            }
                        },
                        error: function (data) {
                            hideLoading();
                            alert(data.message);
                            console.log(data);
                        }
                    });
                }
            }
        });
    });
});