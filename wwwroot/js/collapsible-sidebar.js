window.sidebarFunctions =
{
    ShowMsg: function (message) {
        window.alert(message);
    },

    collapseNav: function (xyz) {
        var sidebar = window.document.getElementById("mySidebar");
        var main = window.document.getElementById("main");

        alert(sidebar.width);

        if (sidebar.style.width === "250px") {
            sidebar.style.width = "50px";
            main.style.marginLeft = "250px";
        }
        else {
            sidebar.style.width = "250px";
            main.style.marginLeft = "250px";
        }
    },

    closeNav: function(xyz) {
        document.getElementById("mySidebar").style.width = "0";
        document.getElementById("main").style.marginLeft = "0";
    }
}


function registerCollapseSidebar(container) {
    
    var button = $("#btnCollapse");
    if (button) {
        button.click(function() {
            var sidebar = $("#mySidebar");
            var main = $("#main");

            //alert("jquery" + sidebar.width());

            if (sidebar.width() === 280) {
                sidebar.width(80);
                //main.style.marginLeft = "250";
            }
            else {
                sidebar.width(280);
                //main.style.marginLeft = "250px";
            }
        });
    }
}

