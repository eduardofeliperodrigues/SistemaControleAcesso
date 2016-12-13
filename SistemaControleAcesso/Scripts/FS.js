var fs = {
    preparaListener: function (func) {

        $.each(func, function (a, item) {
            if (item.hasOwnProperty("on")) {
                $.each(item.on, function (b, evento) {
                    $(document).bind(evento, item.funcao);
                });

            }
            if (item.hasOwnProperty("blur")) {
                $.each(item.blur, function (c, campo) {
                    $(campo).bind("blur", item.funcao);
                });

            }

        });
    }

}