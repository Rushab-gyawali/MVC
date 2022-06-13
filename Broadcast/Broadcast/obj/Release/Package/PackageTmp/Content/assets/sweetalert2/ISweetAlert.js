

//Swal.fire({
//    title: 'Pending Booking?',
//    text: " Do you want to cancel the Previous Booking !",
//    icon: 'warning',
//    showCancelButton: true,
//    confirmButtonColor: '#3085d6',
//    cancelButtonColor: '#d33',
//    confirmButtonText: 'No',
//    cancelButtonText: 'Yes',
//}).then((result) => {
//    if (result.value) {
//        alert("No")

//    }
//    else {
//        alert("Yes")


//    }
//});

function AlertMsg(msg, errorCode) {
    var title = "";
    if (errorCode == "0" || errorCode == 0) {
        msgtype = "success";
    }
    else if (errorCode == "1" || errorCode == 1) {
        msgtype = "error";
    }
    else if (errorCode == "2" || errorCode == 2) {
        msgtype = "warning";
    }
    else {
        msgtype = "question";
    }

    Swal.fire(
        title,
        msg,
        msgtype //'question'
    ).then((result) => {

    });
}

function ShowAlert(msg, errorCode, reload) {
    var title = "";
    if (errorCode == "0" || errorCode == 0) {
        msgtype = "success";
    }
    else if (errorCode == "1" || errorCode == 1) {
        msgtype = "error";
    }
    else if (errorCode == "2" || errorCode == 2) {
        msgtype = "warning";
    }
    else {
        msgtype = "question";
    }

    Swal.fire(
        title,
        msg,
        msgtype //'question'
    ).then((result) => {
        if (reload)
            location.reload();
    });
}
function ShowAlertAndRedirect(msg, errorCode, url) {
    var title = "";
    if (errorCode == "0" || errorCode == 0) {
        msgtype = "success";
    }
    else if (errorCode == "1" || errorCode == 1) {
        msgtype = "error";
    }
    else if (errorCode == "2" || errorCode == 2) {
        msgtype = "warning";
    }
    else {
        msgtype = "question";
    }

    Swal.fire(
        title,
        msg,
        msgtype //'question'
    ).then((result) => {
        document.location.href = '/' + url;
    });
}
function ShowAlertContinueConfirm(title, msg, goBackUrl) {

    Swal.fire({
        title: title,//'Are you sure?',
        text: msg,//"You won't be able to revert this!",
        icon: 'success',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Continue',
    }).then((result) => {
        if (result.value) {
            clearform();
        }
        else {
            document.location.href = '/' + goBackUrl;
        }
    });
}

function ShowAlertWithTitle(title, msg, msgtype) {
    Swal.fire(
        title,
        msg,
        msgtype //'question'
    )
}
//A modal with a title, an error icon, a text, and a footer
function ShowAlertWithTitleAndIcon(title, msg, icontype, footer) {
    Swal.fire({
        icon: icontype,//'error',
        title: title,//'Oops...',
        text: msg,//'Something went wrong!',
        footer: footer//'<a href>Why do I have this issue?</a>'
    })
}
function ShowAlertCustomHTML(htmlTitle, iconType, htmlMsg, showCloseBtn, showCancelBtn, focusConfirm, confirmBtnText, confirmBtnArialLabel, cancelBtnText, cancelBtnArialLabel) {
    Swal.fire({
        title: htmlTitle, //'<strong>HTML <u>example</u></strong>',
        icon: iconType, //'info',
        html: htmlMsg,
        //'You can use <b>bold text</b>, ' +
        //'<a href="//sweetalert2.github.io">links</a> ' +
        //'and other HTML tags',
        showCloseButton: showCloseBtn, //true,
        showCancelButton: showCancelBtn, //true,
        focusConfirm: focusConfirm, //false,
        confirmButtonText: confirmBtnText,
        //'<i class="fa fa-thumbs-up"></i> Great!',
        confirmButtonAriaLabel: confirmBtnArialLabel, //'Thumbs up, great!',
        cancelButtonText: cancelBtnText,
        //'<i class="fa fa-thumbs-down"></i>',
        cancelButtonAriaLabel: cancelBtnArialLabel //'Thumbs down'
    });
}
function ShowAlertOnTopCorner(iconType, msg) {
    Swal.fire({
        position: 'top-end',
        icon: iconType, //'success',
        title: msg, //'Your work has been saved',
        showConfirmButton: false,
        timer: 1500
    })
}
function ShowConfirmDialog(title, msg, icontype, showCancelBtn, confirmBtnColor, cancelBtnColor, confirmBtnText, successCallBackFuntion, params, ctrl) {

    Swal.fire({
        title: (title != "") ? title : 'Are you sure?',
        text: msg,//"You won't be able to revert this!",
        icon: (icontype != "") ? iconType : 'warning',
        showCancelButton: (showCancelBtn) ? showCancelBtn : true,
        confirmButtonColor: (confirmBtnColor != "") ? confirmBtnColor : '#3085d6',
        cancelButtonColor: (cancelBtnColor != "") ? cancelBtnColor : '#d33',
        confirmButtonText: (confirmBtnText != "") ? confirmBtnText : 'Yes'
    }).then((result) => {
        if (result.value) {
            //Swal.fire(
            //    'Deleted!',
            //    'Your file has been deleted.',
            //    'success'
            //)

            successCallBackFuntion(params, ctrl);
        }
        else if (result.dismiss == 'cancel') {
            var paramarr = params.split(":");
            var action = paramarr[1];
            //alert(action);
            if (action == 'true') {
                $(ctrl).prop("checked", false);
            }
            else {
                $(ctrl).prop("checked", 'checked');
            }
        }
    });
}
function ShowConfirm(title, msg, icontype, showCancelBtn, confirmBtnColor, cancelBtnColor, confirmBtnText, successCallBackFuntion, cancelCallBackFunction) {

    Swal.fire({
        title: (title != "") ? title : 'Are you sure?',
        text: msg,//"You won't be able to revert this!",
        icon: (icontype != "") ? iconType : 'warning',
        showCancelButton: (showCancelBtn) ? showCancelBtn : true,
        confirmButtonColor: (confirmBtnColor != "") ? confirmBtnColor : '#3085d6',
        cancelButtonColor: (cancelBtnColor != "") ? cancelBtnColor : '#d33',
        confirmButtonText: (confirmBtnText != "") ? confirmBtnText : 'Yes'
    }).then((result) => {
        if (result.value) {
            //swalWithBootstrapButtons.fire(
            //    'Deleted!',
            //    'Your file has been deleted.',
            //    'success'
            //)
            successCallBackFuntion()
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            //swalWithBootstrapButtons.fire(
            //    'Cancelled',
            //    'Your imaginary file is safe :)',
            //    'error'
            //)
            if (cancelCallBackFunction != "")
                cancelCallBackFunction()
        }
    });
}
function ShowAlertDialog1(title, msg, icontype, showCancelBtn, confirmBtnText, cancelBtnText, successCallBackFuntion, cancelCallBackFunction) {
    const swalWithBootstrapButtons = Swal.mixin({
        customClass: {
            confirmButton: 'btn btn-success',
            cancelButton: 'btn btn-danger'
        },
        buttonsStyling: false
    })

    swalWithBootstrapButtons.fire({

        title: (title != "") ? title : 'Are you sure?',
        text: msg,//"You won't be able to revert this!",
        icon: (icontype != "") ? iconType : 'warning',
        showCancelButton: (showCancelBtn) ? showCancelBtn : true,
        //confirmButtonColor: (confirmBtnColor != "") ? confirmBtnColor : '#3085d6',
        //cancelButtonColor: (cancelBtnColor != "") ? cancelBtnColor : '#d33',
        confirmButtonText: (confirmBtnText != "") ? confirmBtnText : 'Yes',

        reverseButtons: true
    }).then((result) => {
        if (result.value) {
            //swalWithBootstrapButtons.fire(
            //    'Deleted!',
            //    'Your file has been deleted.',
            //    'success'
            //)
            successCallBackFuntion()
        } else if (
            /* Read more about handling dismissals below */
            result.dismiss === Swal.DismissReason.cancel
        ) {
            //swalWithBootstrapButtons.fire(
            //    'Cancelled',
            //    'Your imaginary file is safe :)',
            //    'error'
            //)
            if (cancelCallBackFunction != "")
                cancelCallBackFunction()
        }
    })

}

function ShowAlertAutoClose(title, msg) {
    let timerInterval
    Swal.fire({
        title: title, //'Auto close alert!',
        html: msg, //'I will close in <b></b> milliseconds.',
        timer: 2000,
        timerProgressBar: true,
        onBeforeOpen: () => {
            Swal.showLoading()
            //timerInterval = setInterval(() => {
            //    const content = Swal.getContent()
            //    if (content) {
            //        const b = content.querySelector('b')
            //        if (b) {
            //            b.textContent = Swal.getTimerLeft()
            //        }
            //    }
            //}, 100)
        },
        onClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        /* Read more about handling dismissals below */
        if (result.dismiss === Swal.DismissReason.timer) {
            /*console.log('I was closed by the timer')*/
        }
    })
}
function ShowAlertAjax() {
    Swal.fire({
        title: 'Submit your Github username',
        input: 'text',
        inputAttributes: {
            autocapitalize: 'off'
        },
        showCancelButton: true,
        confirmButtonText: 'Look up',
        showLoaderOnConfirm: true,
        preConfirm: (login) => {
            return fetch(`//api.github.com/users/${login}`)
                .then(response => {
                    if (!response.ok) {
                        throw new Error(response.statusText)
                    }
                    return response.json()
                })
                .catch(error => {
                    Swal.showValidationMessage(
                        `Request failed: ${error}`
                    )
                })
        },
        allowOutsideClick: () => !Swal.isLoading()
    }).then((result) => {
        if (result.value) {
            Swal.fire({
                title: `${result.value.login}'s avatar`,
                imageUrl: result.value.avatar_url
            })
        }
    })
}
function ShowQueueAlert() {
    Swal.mixin({
        input: 'text',
        confirmButtonText: 'Next &rarr;',
        showCancelButton: true,
        progressSteps: ['1', '2', '3']
    }).queue([
        {
            title: 'Question 1',
            text: 'Chaining swal2 modals is easy'
        },
        'Question 2',
        'Question 3'
    ]).then((result) => {
        if (result.value) {
            const answers = JSON.stringify(result.value)
            Swal.fire({
                title: 'All done!',
                html: `
        Your answers:
        <pre><code>${answers}</code></pre>
      `,
                confirmButtonText: 'Lovely!'
            })
        }
    })

}

function ShowInputAlert(alertTitle, inputType, inputOpt, inputPlaceholder, showCancelBtn) {

    Swal.fire({
        title: alertTitle,
        input: inputType,
        inputOptions: inputOpt,
        inputPlaceholder: inputPlaceholder,
        showCancelButton: showCancelBtn,
        inputValidator: (value) => {
            return new Promise((resolve) => {
                if (value != '') {
                    resolve()
                } else {
                    resolve('You need to select seat class :)')
                }
            })
        }
    }).then(function (result) {
        if (result.value) {
            return true;
        } else if (result.dismiss == 'cancel') {
            $("#chkRouteStationEnableDisable").prop("checked", false);
        }
    });


}
function ShowInputAlert1(alertTitle, inputType, inputPlaceholder, showCancelBtn, successCallBackFuntion) {

    Swal.fire({
        title: alertTitle,
        input: inputType,
        inputPlaceholder: inputPlaceholder,
        showCancelButton: showCancelBtn,
        inputValidator: (value) => {
            return new Promise((resolve) => {
                if (value != '') {
                    resolve()
                } else {
                    resolve('Draft Title is required.')
                }
            })
        }
    }).then(function (result) {
        if (result.value) {
            successCallBackFuntion(result.value);
        } else if (result.dismiss == 'cancel') {

        }
    });


}
function ShowHTMLInputAlert() {
    //$.fn.modal.Constructor.prototype._enforceFocus = function () { };
    //Swal.fire({
    //    title: 'Multiple inputs',
    //    html:
    //        '<input id="swal-input1" class="swal2-input">' +
    //        '<input id="swal-input2" class="swal2-input">',
    //    focusConfirm: false,
    //    preConfirm: function () {
    //        return new Promise(function (resolve) {
    //            // Validate input
    //            if ($('#swal-input1').val() == '' || $('#swal-input2').val() == '') {
    //                swal.showValidationMessage("Enter a value in both fields"); // Show error when validation fails.
    //                swal.enableButtons(); // Enable the confirm button again.
    //            } else {
    //                swal.resetValidationMessage(); // Reset the validation message.
    //                resolve([
    //                    $('#swal-input1').val(),
    //                    $('#swal-input2').val()
    //                ]);
    //            }
    //        })
    //    },
    //    onOpen: function () {
    //        $('#swal-input1').focus()
    //    }
    //}).then(function (result) {
    //    // If validation fails, the value is undefined. Break out here.
    //    if (typeof(result.value) == 'undefined') {
    //        return false;
    //    }
    //    Swal.fire(JSON.stringify(result))
    //}).catch(swal.noop)

    const { value: formValues } = Swal.fire({
        title: 'Multiple inputs',
        html:
            '<input id="swal-input1" class="swal2-input">' +
            '<input id="swal-input2" class="swal2-input">',
        focusConfirm: true,
        preConfirm: () => {
            return [
                document.getElementById('swal-input1').value,
                document.getElementById('swal-input2').value
            ]
        }
    })

    if (formValues) {
        Swal.fire(JSON.stringify(formValues))
    }
}
