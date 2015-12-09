/// <reference path="C:\Users\mgoswami\Documents\Visual Studio 2013\Projects\ZC_IT_TimeTracking\ZC_IT_TimeTracking\Views/Home/_AssignGoal.cshtml" />
/// <reference path="C:\Users\mgoswami\Documents\Visual Studio 2013\Projects\ZC_IT_TimeTracking\ZC_IT_TimeTracking\Views/Home/_AssignGoal.cshtml" />
/// <reference path="C:\Users\mgoswami\Documents\Visual Studio 2013\Projects\ZC_IT_TimeTracking\ZC_IT_TimeTracking\Views/Home/_AssignGoal.cshtml" />
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
            UnitOfMeasurement: {
                minlength: 3,
                required: true
            },
            MeasurementValue: {
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
            if (!isCreate) GoalData.Goal_MasterID = updateGoalId;
            GoalData.GoalTitle = $("#GoalTitle").val();
            GoalData.GoalDescription = $("#GoalDescription").val();
            GoalData.QuarterYear = $("#GoalYear option:selected").text();
            GoalData.GoalQuarter = $("#GoalQuarter option:selected").text();
            GoalData.UnitOfMeasurement = $("#UnitOfMeasurement").val();
            GoalData.MeasurementValue = $("#MeasurementValue").val();
            GoalData.IsHigherValueGood = $("#IsHigherValueGood").is(":checked");

            if ($("#RuleListTable").find("tr").length > 0) {
                //adding goal rules
                var GoalRules = Array();
                $("#RuleListTable").find("tr").each(function () {
                    var rule = {};
                    rule.Performance_RangeFrom = $(this).find(".RangeFrom").text();
                    rule.Performance_RangeTo = $(this).find(".RangeTo").text();
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
        $('#formInput input,textarea,select').attr('readonly', false);
        var ExistId = $('#GoalQuarter option').attr('id');
        $('#GoalQuarter option').text(ExistId);
    });

    //loading overlay events
    hideLoading = function () { $("#loading").hide(); }
    showLoading = function () { $("#loading").show(); }

    //goal view
    ViewGoal = function (id) {
        ResetForm();
        $("#submitButton").attr("disabled", true);
        $('#formInput input,textarea,select').attr('readonly', true);
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
                    $("#UnitOfMeasurement").val(goal.UnitOfMeasurement);
                    $("#MeasurementValue").val(goal.MeasurementValue);
                    if (goal.IsHigherValueGood)
                        $("#IsHigherValueGood").attr("checked", true);
                    else
                        $("#IsHigherValueGood").attr("checked", false);

                    //rules filling
                    $('#RuleListTable').html("");
                    $(rules).each(function () {
                        //console.log(this);
                        $('#RuleListTable').append('<tr id="rule' + this.Goal_RuleID + '"><td class="col-md-3 RangeFrom">' + this.Performance_RangeFrom + '</td><td class="col-md-3 RangeTo">' + this.Performance_RangeTo + '</td><td class="col-md-3 Rating">' + this.Rating + '</td><td class="col-md-1" id="Action"><span id="Edit" onclick="EditGoalRule(rule' + this.Goal_RuleID + ')" class="glyphicon glyphicon-pencil"/>&nbsp;<span onclick="RemoveGoalRule(rule' + this.Goal_RuleID + ')" class="glyphicon glyphicon-remove" /></td></tr>');
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
        $('#formInput input,textarea,select').attr('readonly', false);
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
                    $("#UnitOfMeasurement").val(goal.UnitOfMeasurement);
                    $("#MeasurementValue").val(goal.MeasurementValue);
                    if (goal.IsHigherValueGood)
                        $("#IsHigherValueGood").attr("checked", true);
                    else
                        $("#IsHigherValueGood").attr("checked", false);
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
            if (RangeFrom == "" || RangeTo == "" || Rating == "")
                bootbox.alert("Fill All The Field.!");
            else {
                $('#RuleListTable').append('<tr id="rule' + count + '"><td class="col-md-3 RangeFrom">' + RangeFrom + '</td><td class="col-md-3 RangeTo">' + RangeTo + '</td><td class="col-md-3 Rating">' + Rating + '</td><td class="col-md-1"><span id="Edit" data-toggle="tooltip" data-placement="bottom" title="Edit Rule" onclick="EditGoalRule(rule' + count + ')" class="glyphicon glyphicon-pencil"/>&nbsp;<span class="glyphicon glyphicon-remove" data-toggle="tooltip" data-placement="bottom" title="Delete Rule" onclick="RemoveGoalRule(rule' + count + ')" /></td></tr>');
                $("#RangeFrom").val(null);
                $("#RangeTo").val(null);
                $("#Rating").val(null);
            }
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
        $('.SelectedGoal').prop('checked', this.checked);
        if ($('.SelectedGoal:checked').length > 0)
            $("#DeleteMultiGoals").show();
        else
            $("#DeleteMultiGoals").hide();
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
                                bootbox.alert(data.message, function () {
                                    location.reload(true);
                                });
                            }
                        },
                        error: function (data) {
                            hideLoading();
                            if (data.readyState == 0) {
                                bootbox.alert("Please check your internet connection!");
                            }
                            console.log(data);
                        }
                    });
                }
            }
        });
    });
    $("#SearchByTitle").click(function () {
        var txt = $("#SearchText").val().trim();
        if (txt.length > 0)
            $("#TitleString").val(txt);
        else
            $("#TitleString").val(null);
        $("#PageForm").submit();
    });
    $("#ClearTitleSearch").click(function () {
        $("#TitleString").val(null);
        $("#PageForm").submit();
    });

    //Assign Goal region

    $("#ButtonAssignGoal").click(function (e) {
        e.preventDefault();
        window.location.href = '/Home/AssignGoal';
    });
    //Get Description from Selected Title
    $("#Goal_MasterID").change(function (e) {
        e.preventDefault();
        var TitleID = $(this).find('option:selected').val();
        if (TitleID == "")
            $("#GoalDescription").val('');
        $.ajax({
            url: "GetDescription",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({ TitleID: TitleID }),
            beforeSend: showLoading(),
            success: function (dt) {
                hideLoading();
                //console.log(dt);
                if (dt.success)
                    $("#GoalDescription").val(dt.TitleData);
            },
            error: function (dt) {
                hideLoading();
                if (dt.readyState == 0) {
                    bootbox.alert("Please check your internet connection!");
                }
            }
        });
    });
    //Assign form Validation
    var AssignValidation = $("#GoalAssignForm").validate({
        rules: {
            GoalTitle: {
                required: true
            },
            GoalDescription: {
                required: true
            },
            Team: {
                required: true
            },
            TeamMember: {
                required: true
            },
            Weight: {
                required: true
            }
        },
        messages: {
            GoalDescription: {
                required: "First Select Title From Above list..!"
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

    //Get TeamMember from Selected Team
    $("#TeamID").focus(function () {
        $(this).attr("oldValue", $(this).val());
    }).change(function (e) {
        e.preventDefault();
        var TeamID = $(this).find('option:selected').val();
        var Weight = parseFloat($("#Weight").val());
        if (isNaN(Weight)) {
            $("#TeamID").val($(this).attr("oldValue"));
            bootbox.alert("Please enter the weight for goal!");
            return;
        }
        var GoalID = $("#Goal_MasterID option:selected").val();
        $.ajax({
            url: "GetTeamMember",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({ TeamID: TeamID, Weight: Weight, GoalID: GoalID }),
            beforeSend: showLoading(),
            success: function (dt) {
                hideLoading();
                //console.log(dt)
                if (dt.success) {
                    console.log(dt);
                    $("#TeamMember").html('');
                    for (var val in dt.TeamMember) {
                        $("#TeamMember").append("<option value=" + dt.TeamMember[val].ResourceID + ">" + dt.TeamMember[val].FirstName + " " + dt.TeamMember[val].LastName + "</option>");
                    }
                }
            },
            error: function (dt) {
                hideLoading();
                if (dt.readyState == 0) {
                    bootbox.alert("Please check your internet connection!");
                }
            }
        });
    });

    //Assign Goal To Resourse
    $("#ButtonAssign").click(function (e) {
        e.preventDefault();
        if ($("#GoalAssignForm").valid()) {
            var AssignData = {};
            AssignData.ResourceID = [];
            var rid = $("#TeamMember option:selected").each(function () {
                AssignData.ResourceID.push(this.value);
            });
            AssignData.Goal_MasterID = $("#Goal_MasterID option:selected").val();
            AssignData.Weight = $("#Weight").val();

            //Assign data
            $.ajax({
                url: "/Home/AssignGoal",
                type: "POST",
                dataType: "json",
                contentType: "application/json",
                data: JSON.stringify({ AssignData: AssignData }),
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
            console.log(AssignData.ResourceID);
        }
    });
    //View AssignGoal
    $("#ButtonViewAssignGoal").click(function () {
        window.location.href = "/Home/ViewAssignGoal";
    });

    $("#TeamMemberName").change(function () {
        var id = $(this).val();
        console.log(id);
        if (id != "") {
            $("#ResId").val(id);
        }
    });

    $("#TeamName").change(function (e) {
        e.preventDefault();
        var TeamID = $(this).find('option:selected').val();
        console.log(TeamID);
        $.ajax({
            url: "ViewAssignGoal",
            type: "POST",
            dataType: "json",
            contentType: "application/json",
            data: JSON.stringify({ TeamID: TeamID }),
            beforeSend: showLoading(),
            success: function (dt) {
                hideLoading();
                //console.log(dt)
                if (dt.success) {
                    console.log(dt);
                    $("#TeamMemberName").html('');
                    $("#TeamMemberName").html('<option value="-1">--Select TeamMember--</option>');
                    for (var val in dt.TeamMember) {
                        $("#TeamMemberName").append("<option value=" + dt.TeamMember[val].ResourceID + ">" + dt.TeamMember[val].FirstName + " " + dt.TeamMember[val].LastName + "</option>");
                    }
                }
            },
            error: function (dt) {
                hideLoading();
                if (dt.readyState == 0) {
                    bootbox.alert("Please check your internet connection!");
                }
            }
        });
    });

    $("#recordInPageDDL").change(function () {

    });

    EditAssignedGoalWight = function (id) {                
        $.ajax({
            url: "/Home/GetAssignedGoal",
            type: "POST",
            dataType: "Json",
            contentType: "application/json",
            data: JSON.stringify({ AssignId: id }),
            beroreSend: showLoading(),
            success: function (dt) {
                hideLoading();
                if (dt.success) {
                    EditWeight(dt.Data);
                }
                else {
                    bootbox.alert(dt.message);
                }
            },
            error: function (dt) {
                hideLoading();
                if (dt.readyState == 0) {
                    bootbox.alert("Please check your internet connection!");
                }
            }
        });

        EditWeight = function (data) {
            var weight = prompt("Update Weight", data.Weight);
            var GoalID = data.Goal_MasterID;
            var ResourceId = data.ResourceID;
            $.ajax({
                url: "/Home/EditAssignedGoal",
                type: "POST",
                dataType: "Json",
                contentType: "application/json",
                data: JSON.stringify({ GoalID: GoalID, Weight: weight, ResourceId: ResourceId }),
                beroreSend: showLoading(),
                success: function (dt) {
                    hideLoading();
                    location.reload(true);
                },
                error: function (dt) {
                    hideLoading();
                    if (dt.readyState == 0) {
                        bootbox.alert("Please check your internet connection!");
                    }
                }
            });           
        }
    }

    DeleteAssignedGoalWeight = function (id) {
        bootbox.confirm("Are you sure to delete?", function (r) {
            if (r) {
                $.ajax({
                    url: "/Home/DeleteAssignedGoal",
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
});