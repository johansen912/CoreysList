
$(document).ready(function($){
    $('#login').validate(
    {
        onkeyup: false,
        onclick: false,
        onfocusout: false,

        rules:
      {
          Email: {
              required: true,
              email: true
          },
          Password: { required: true }
      },
        messages:
        {
            Email: {
                required: "*",
                email: "Not a valid email"
            },
            Password: {
                required: "*"
            }
        }
    });


    $('#createAccount').validate(
    {
        onkeyup: false,
        onclick: false,
        onfocusout: false,

        rules:
      {
          Email: {
              required: true,
              maxlength: 40,
              email: true
          },
          FirstName: {
              rangelength: [2,25],
              required: true
          },
          LastName: {
              rangelength: [2, 35],
              required: true
          },
          PhoneNumber: {
              pattern: /^[1-9]\d{2}-\d{3}-\d{4}/,
              required: true
          },
          Password: {
              pattern: /^[a-zA-Z]\w{7,14}$/,
              required: true
          },
          confirmPassword: {
              required: true,
              equalTo: "#Password"

          }
      },
        messages:
        {
            Email: {
                required: "*",
                email: "Not a valid email",
                maxlength: "Must be under 40 characters"
            },
            Password: {
                required: "*",
                pattern: "Between 8-15 characters"
            },
            confirmPassword: {
                required: "*",
                equalTo: "Passwords must match"
            },
            FirstName: {
                required: "*",
                rangelength: "Must be between 2 and 25 charcters"
            },
            LastName: {
                required: "*",
                rangelength: "Must be between 2 and 35 charcters"
            },
            PhoneNumber: {
                required: "*",
                pattern: "###-###-#### Format only",              
            }
        }
    });

    $('#createAccount').submit(function () {
        return $(this).valid();
    });
});