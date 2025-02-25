let alert = {
    ConfirmDelete: (callBack) => {
        Swal.fire({
            title: "Are you sure?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#3085d6",
            cancelButtonColor: "#d33",
            confirmButtonText: "Yes, delete it!"
        }).then((result) => {
            if (result.isConfirmed) {
                callBack(result);
               
            }
        });
    },
    Success: (title,text) => {
        Swal.fire({
            title: title,
            text: text,
            icon: "success"
        });
    },
    Error: (title, text) => {
        Swal.fire({
            title: title,
            text: text,
            icon: "error"
        });
    },

}