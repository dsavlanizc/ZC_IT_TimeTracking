$(function () {
    hideLoading = function () { $("#loading").hide(); }
    showLoading = function () { $("#loading").show(); }
    ViewGoal = function (id) {
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
                console.log(dt);
                var data = JSON.parse(dt);
                var goal = data.goal;
                var quarter = data.quarter;
                $("#GoalTitle").val(goal.GoalTitle);
                $("#GoalDescription").val(goal.GoalDescription);
                $("#GoalYear option:selected").text(quarter.YEAR);
                $("#GoalQuarter option:selected").text(quarter.Quater);
                $("#GoalUnit").val(goal.UnitOfMeasurement);
                $("#GoalUnitValue").val(goal.MeasurementValue);
                if (goal.IsHigherValueGood)
                    $("#IsTrue").attr("checked", true);
                else
                    $("#IsTrue").attr("checked", false);
            },
            error: function (dt) {
                hideLoading();
                console.log(dt);
            }
        });
    }
    $("#GoalCreateForm").submit(function (sd) {
        sd.preventDefault();
        if ($(this).valid()) {
            alert('the form is valid');
        }
        else {
            alert('the form is not valid');
        }
    });

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
    addToList = function() {
        if (count > -1) {
            count += 1;
            var RangeFrom = $("#RangeFrom").val();
            var RangeTo = $("#RangeTo").val();
            var Rating = $("#Rating").val();
            $('#RuleListTable').append('<tr id="rule' + count + '"><td class="col-md-3">' + RangeFrom + '</td><td class="col-md-3">' + RangeTo + '</td><td class="col-md-3">' + Rating + '</td><td class="col-md-1"><span id="Edit" onclick="editRule(rule' + count + ')" class="glyphicon glyphicon-pencil"/>&nbsp;<span onclick="removeRule(rule' + count + ')" class="glyphicon glyphicon-remove" /></td></tr>');
            $("#RangeFrom").val(null);
            $("#RangeTo").val(null);
            $("#Rating").val(null);
        }
    }
    editRule = function(id) {
        var RangeFrom = $(id).find("td:nth-child(1)").html();
        var RangeTo = $(id).find("td:nth-child(2)").html()
        var Rating = $(id).find("td:nth-child(3)").html();
        $("#RangeFrom").val(RangeFrom);
        $("#RangeTo").val(RangeTo);
        $("#Rating").val(Rating);
        $(id).remove();
    }
    removeRule = function(id) {
        $(id).remove();
    }
});