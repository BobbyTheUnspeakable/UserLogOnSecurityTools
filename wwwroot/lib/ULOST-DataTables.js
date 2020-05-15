$(function() {
    window.ULOST = window.ULOST || {};
    window.ULOST.DataTables = window.ULOST.DataTables ||
    {

        config: {
            
        },

        dtConfig: {
            buttons: [
                {
                    text: "Edit",
                    action: function (e, dt, node, config) {
                        var ids = window.ULOST.DataTables.getSelectedIds(dt);
                        if (ids.length === 0 || ids.length > 1) {
                            alert("Please select a single row to edit.");
                            return;
                        }
                        window.ULOST.DataTables.editItem(ids[0]);
                    }
                }
            ]
        },

        editItem: function (id) {
            window.location.href = '/GeocortexIdentityManager/EditUser/' + id;
        },

        getSelectedIds: function (dt, dataname) {
            var selected = dt.rows({ selected: true });
            var jq = selected.nodes().to$();
            var ids = [];
            dataname = dataname || "id";
            jq.each(function () {
                var row = $(this);
                var id = row.data(dataname);
                if (id != undefined) {
                    ids.push(id);
                    return true;
                }
                // fallback
                dataname = "id";
                id = $(this).data(dataname);
                if (id == undefined)
                    return false;
                ids.push(id);
            });
            if (ids.length > 0)
                return ids;

            data = selected.data();
            for (var i = 0; i < data.length; ++i) {
                ids.push(data[i][0]);
            }
            return ids;
        },

        displayTable: function (divId) {
            $(".data-table").DataTable({
                select: true,
                buttons: window.ULOST.DataTables.dtConfig.buttons,
                dom: "lfrtBip",
                destroy: true
            });
        },

        init: function() {
            $(".tables").each(function() {
                var divId = $(this).attr("id");
                window.ULOST.DataTables.displayTable(divId);
            });
        }

    }

    window.ULOST.DataTables.init();

});