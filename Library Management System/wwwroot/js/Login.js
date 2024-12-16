const formOpenBtn = document.querySelector("#form-open"),
    home = document.querySelector(".home"),
    formContainer = document.querySelector(".form_container"),
    formCloseBtn = document.querySelector(".form_close"),
    signupBtn = document.querySelector("#signup"),
    loginBtn = document.querySelector("#login"),
    pwShowHide = document.querySelectorAll(".pw_hide");

formOpenBtn.addEventListener("click", () => home.classList.add("show"));
formCloseBtn.addEventListener("click", () => home.classList.remove("show"));

pwShowHide.forEach((icon) => {
    icon.addEventListener("click", () => {
        let getPwInput = icon.parentElement.querySelector("input");
        if (getPwInput.type === "password") {
            getPwInput.type = "text";
            icon.classList.replace("uil-eye-slash", "uil-eye");
        } else {
            getPwInput.type = "password";
            icon.classList.replace("uil-eye", "uil-eye-slash");
        }
    });
});

signupBtn.addEventListener("click", (e) => {    
    e.preventDefault();
    formContainer.classList.add("active");
});
loginBtn.addEventListener("click", (e) => {
    e.preventDefault();
    formContainer.classList.remove("active");
});
//signupLink.addEventListener("click", (e) => {
//    e.preventDefault();
//    formContainer.classList.add("active");
//});




$(document).ready(function () {
  
    $('#form-open').click(function () {
        $('.form_container').fadeIn();
    });

     
    $('.form_close').click(function () {
        $('.form_container').fadeOut();
    });

   
    $('.slide.signup').click(function () {
        $('#signup').prop('checked', true);
    });

    $('.slide.login').click(function () {
        $('#login').prop('checked', true);
    });



});

$(document).ready(function () {
    const loginText = $(".title-text .login");
    const loginForm = $("form.login");
    const loginBtn = $("label.login");
    const signupBtn = $("label.signup");
    const signupLink = $("form .signup-link a");

 
    $('#form-open').click(function () {
        $('.form_container').fadeIn();
    });

 
    $('.form_close').click(function () {
        $('.form_container').fadeOut();
    });
 
    signupBtn.click(function () {
        loginForm.css("marginLeft", "-50%");
        loginText.css("marginLeft", "-50%");
    });

    loginBtn.click(function () {
        loginForm.css("marginLeft", "0%");
        loginText.css("marginLeft", "0%");
    });

 
    $('#signup-link').click(function (event) {
        event.preventDefault();
        signupBtn.click();
    });

 
    $('#login-link').click(function (event) {
        event.preventDefault();
        loginBtn.click();
    });
});
 
signupBtn.onclick = () => {
    loginForm.style.marginLeft = "-50%";
    loginText.style.marginLeft = "-50%";
};
loginBtn.onclick = () => {
    loginForm.style.marginLeft = "0%";
    loginText.style.marginLeft = "0%";
};
signupLink.onclick = () => {
    signupBtn.click();
    return false;
};


$(document).ready(function () {
    function validateLoginForm() {
        let isValid = true;

        // Clear previous error messages and border styles
        $('.text-danger').text('');
        $('input').removeClass('error-border');

        const email = $('#LoginEmail').val().trim();
        const password = $('#LoginPassword').val().trim();
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (email === '') {
            $('#LoginEmailError').text('Email is required.');
            $('#LoginEmail').addClass('error-border');
            isValid = false;
        } else if (!emailRegex.test(email)) {
            $('#LoginEmailError').text('Please enter a valid email address.');
            $('#LoginEmail').addClass('error-border');
            isValid = false;
        }

        if (password === '') {
            $('#LoginPasswordError').text('Password is required.');
            $('#LoginPassword').addClass('error-border');
            isValid = false;
        }

        return isValid;
    }

    function validateSignupForm() {
        let isValid = true;

        // Clear previous error messages and border styles
        $('.text-danger').text('');
        $('input').removeClass('error-border');

        const email = $('#SignupEmail').val().trim();
        const password = $('#SignupPassword').val().trim();
        const confirmPassword = $('#ConfirmPassword').val().trim();
        const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

        if (email === '') {
            $('#SignupEmailError').text('Email is required.');
            $('#SignupEmail').addClass('error-border');
            isValid = false;
        } else if (!emailRegex.test(email)) {
            $('#SignupEmailError').text('Please enter a valid email address.');
            $('#SignupEmail').addClass('error-border');
            isValid = false;
        }

        if (password === '') {
            $('#SignupPasswordError').text('Password is required.');
            $('#SignupPassword').addClass('error-border');
            isValid = false;
        }

        if (confirmPassword === '') {
            $('#ConfirmPasswordError').text('Confirm Password is required.');
            $('#ConfirmPassword').addClass('error-border');
            isValid = false;
        } else if (password !== confirmPassword) {
            $('#ConfirmPasswordError').text('Passwords do not match.');
            $('#ConfirmPassword').addClass('error-border');
            isValid = false;
        }

        return isValid;
    }

    $('#loginForm').on('submit', function (e) {
        if (!validateLoginForm()) {
            e.preventDefault();
        }
    });

    $('#signupForm').on('submit', function (e) {
        if (!validateSignupForm()) {
            e.preventDefault();
        }
    });

    // Clear error message and border when input changes
    $('input').on('input', function () {
        const id = $(this).attr('id');
        $(`#${id}Error`).text('');
        $(this).removeClass('error-border');
    });
});