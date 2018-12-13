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
