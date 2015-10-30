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
});