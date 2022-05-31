// Code By Webdevtrick ( https://webdevtrick.com )
$(document).ready(function () {

    $('#password').keyup(function () {
        var password = $('#password').val();
        if (checkStrength(password) == false) {
            $('#sign-up').attr('disabled', true);
        }

        checkWordSequences(password);
    });
    $('#confirm-password').blur(function () {
        if ($('#password').val() !== $('#confirm-password').val()) {
            $('#popover-cpassword').removeClass('hide');
            $('#sign-up').attr('disabled', true);
        } else {
            $('#popover-cpassword').addClass('hide');
        }
    });


    var IsPasswordStrong = false;

    function checkWordSequences(password) {
        // Word Sequences check
        if (hasWordSequences(password)) {
            IsPasswordStrong = false;
            $("#contains-sequences").css("display", "block");

        } else {
            $("#contains-sequences").css("display", "none");
        }
    }


    function checkStrength(password) {

        var strength = 0;
        //If password contains uppercase characters, increase strength value.
        if (password.match(/(.*[A-Z])/)) {
            strength += 1;
            $('.upper-case').addClass('text-success');
            $('.upper-case i').removeClass('fa-times').addClass('fa-check');
            $('#popover-password-top').addClass('hide');

        } else {
            $('.upper-case').removeClass('text-success');
            $('.upper-case i').addClass('fa-times').removeClass('fa-check');
            $('#popover-password-top').removeClass('hide');
        }
        //If password contains lower characters, increase strength value.
        if (password.match(/(.*[a-z])/)) {
            strength += 1;
            $('.low-case').addClass('text-success');
            $('.low-case i').removeClass('fa-times').addClass('fa-check');
            $('#popover-password-top').addClass('hide');


        } else {
            $('.low-case').removeClass('text-success');
            $('.low-case i').addClass('fa-times').removeClass('fa-check');
            $('#popover-password-top').removeClass('hide');
        }

        //If it has numbers and characters, increase strength value.
        if (password.match(/(.*[0-9])/)) {
            strength += 1;
            $('.one-number').addClass('text-success');
            $('.one-number i').removeClass('fa-times').addClass('fa-check');
            $('#popover-password-top').addClass('hide');

        } else {
            $('.one-number').removeClass('text-success');
            $('.one-number i').addClass('fa-times').removeClass('fa-check');
            $('#popover-password-top').removeClass('hide');
        }

        //If it has one special character, increase strength value.
        if (password.match(/([!,%,&,@@,#,$,^,*,?,_,~])/)) {
            strength += 1;
            $('.one-special-char').addClass('text-success');
            $('.one-special-char i').removeClass('fa-times').addClass('fa-check');
            $('#popover-password-top').addClass('hide');

        } else {
            $('.one-special-char').removeClass('text-success');
            $('.one-special-char i').addClass('fa-times').removeClass('fa-check');
            $('#popover-password-top').removeClass('hide');
        }

        if (password.length > 7) {
            strength += 1;
            $('.eight-character').addClass('text-success');
            $('.eight-character i').removeClass('fa-times').addClass('fa-check');
            $('#popover-password-top').addClass('hide');

        } else {
            $('.eight-character').removeClass('text-success');
            $('.eight-character i').addClass('fa-times').removeClass('fa-check');
            $('#popover-password-top').removeClass('hide');
        }


        // If value is less than 2

        if (strength == 0) {
            $('#result').removeClass();
            $('#password-strength').addClass('progress-bar-danger');
            $('#result').addClass('text-danger').text('');
            $('#password-strength').css('width', '0%');

            IsPasswordStrong = false;
            disabledSubmitBtn();
        }
        else if (strength == 1) {
            $('#result').removeClass();
            $('#password-strength').addClass('progress-bar-danger');
            $('#result').addClass('text-danger').text('Very Week');
            $('#password-strength').css('width', '10%');

            IsPasswordStrong = false;
            disabledSubmitBtn();

        } else if (strength == 2) {
            $('#result').addClass('good');
            $('#password-strength').removeClass('progress-bar-danger');
            $('#password-strength').addClass('progress-bar-warning');
            $('#result').addClass('text-warning').text('Week');
            $('#password-strength').css('width', '30%');

            IsPasswordStrong = false;
            disabledSubmitBtn();
            return 'Week';
        } else if (strength == 3 ) {
            $('#result').addClass('good');
            $('#password-strength').removeClass('progress-bar-danger');
            $('#password-strength').addClass('progress-bar-warning');
            $('#result').addClass('text-warning').text('Good');
            $('#password-strength').css('width', '50%');

            IsPasswordStrong = false;
            disabledSubmitBtn();
            return 'Good';
        } else if (strength == 4) {
            $('#result').addClass('good');
            $('#password-strength').removeClass('progress-bar-danger');
            $('#password-strength').addClass('progress-bar-warning');
            $('#result').addClass('text-warning').text('Very Good');
            $('#password-strength').css('width', '80%');

            IsPasswordStrong = false;
            disabledSubmitBtn();
            return 'Very Good';
        } else if (strength > 4 & password.length > 7) {
            $('#result').removeClass();
            $('#result').addClass('strong');
            $('#password-strength').removeClass('progress-bar-warning');
            $('#password-strength').addClass('progress-bar-success');
            $('#result').addClass('text-success').text('Strength');
            $('#password-strength').css('width', '100%');

            IsPasswordStrong = true;
            enableSubmitBtn();

            return 'Strong';
        }

    }

    function disabledSubmitBtn() {
        $("#submit-btn-id").prop("disabled", true);
        $("#submit-btn-id").addClass("disabled");
    }

    function enableSubmitBtn() {
        var password = $('#password').val();
        var confirmPassword = $('#confirm-password').val();

        if (IsPasswordStrong && password === confirmPassword) {
            $("#submit-btn-id").prop("disabled", false);
            $("#submit-btn-id").removeClass("disabled");
        } else {
            disabledSubmitBtn();
        }
    }


    $('#confirm-password').keyup(function () {
        enableSubmitBtn();
    });

    var forbiddenSequences = [
        "0123456789", "abcdefghijklmnopqrstuvwxyz", "qwertyuiop", "asdfghjkl",
        "zxcvbnm", "!@@#$%^&*()_+"
    ];

    function hasWordSequences(word) {
        return false;
        var found = false,
            j;
        if (word.length > 2) {
            $.each(forbiddenSequences, function (idx, seq) {
                var sequences = [seq, seq.split('').reverse().join('')];
                $.each(sequences, function (idx, sequence) {
                    for (j = 0; j < (word.length - 2) ; j += 1) { // iterate the word trough a sliding window of size 3:
                        if (sequence.indexOf(word.toLowerCase().substring(j, j + 3)) > -1) {
                            found = true;
                        }
                    }
                });
            });
        }
        return found;
    };

});


