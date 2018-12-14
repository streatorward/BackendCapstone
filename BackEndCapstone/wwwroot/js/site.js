// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

    $(function() {
        $('.toggle').change(function () {
            var self = $(this);
            var url = self.data('url');
            var id = self.attr('id');
            var value = self.prop('checked');

            $.ajax({
                url: url,
                data: { id: id },
                type: 'POST',
                success: function (response) {
                    alert("You did it!");
                }
            });
            $(this).closest('tr').remove();
        });
});

document.addEventListener('DOMContentLoaded', () => {

    // Get all "navbar-burger" elements
    const $navbarBurgers = Array.prototype.slice.call(document.querySelectorAll('.navbar-burger'), 0);

    // Check if there are any navbar burgers
    if ($navbarBurgers.length > 0) {

        // Add a click event on each of them
        $navbarBurgers.forEach(el => {
            el.addEventListener('click', () => {

                // Get the target from the "data-target" attribute
                const target = el.dataset.target;
                const $target = document.getElementById(target);

                const items = document.querySelector(".navbar-menu")

                // Toggle the "is-active" class on both the "navbar-burger" and the "navbar-menu"
                el.classList.toggle('is-active');
                items.classList.toggle('is-active');

            });
        });
    }

});
