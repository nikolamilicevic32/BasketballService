$(document).ready(function () {
    var host = window.location.host;
    var token = null;
    var headers = {};
    var kosarkasiEndpoint = "/api/kosarkasi/";
    var kluboviEndpoint = "/api/klubovi";
    var pretragaEndpoint = "/api/pretraga";
    var formAction = "Create";
    var editingId;

    loadKosarkase();

    $("body").on("click", "#btnDelete", deleteKosarkas);
    $("body").on("click", "#btnEdit", editKosarkas);

    $("#resetujPretragu").click(function () {
        loadKosarkase();
        $("#pretragaStart").val('');
        $("#pretragaKraj").val('');
    });

    $("#odustajanje").click(function () {
       // refreshTable();
        $("#formKosarkasiDiv").css("display", "none");

    });


    $("#pocetnoDugme").click(function () {
        $("#prijavaDiv").css("display", "block");
        $("#registracijaIprijava").css("display", "none");
        $("#dataKosarkasi").css("display", "block");

    });

    function loadKosarkase() {
        var requestUrl = 'https://' + host + kosarkasiEndpoint;
        $.getJSON(requestUrl, setKosarkase);

        var dropdownUrl = 'https://' + host + kluboviEndpoint;
        $.getJSON(dropdownUrl, setDropdown);
    };


    function setKosarkase(data, status) {

        var $container = $("#dataKosarkasi");
        $container.empty();
        console.log(data);
        if (status === "success") {
            // ispis naslova
            var div = $("<div></div>");
            var h1 = $("<h1>Kosarkasi</h1>");
            div.append(h1);
            // ispis tabele
            var table = $("<table class='table table-bordered'></table>");
            if (token) {
                var header = $("<thead><tr><td>Ime i Prezime</td><td>Rodjenje</td><td>Klub</td><td>Utakmice</td><td>Poeni</td><td>Delete</td><td>Edit</td></tr></thead>");
            } else {
                header = $("<thead><tr><td>Ime i Prezime</td><td>Rodjenje</td><td>Klub</td><td>Utakmice</td><td>Poeni</td></tr></thead>");
            }

            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].ImePrezime + "</td><td>" + data[i].GodinaRodjenja + "</td><td>" + data[i].KosarkaskiKlub.Naziv + "</td><td>" + data[i].BrojUtakmica + "</td><td>" + data[i].ProsecanBrPoena + "</td>";
                // prikaz dugmadi za izmenu i brisanje
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id=btnDelete name=" + stringId + ">Delete</button></td>";
                var displayEdit = "<td><button id=btnEdit name=" + stringId + ">Edit</button></td>";
                // prikaz samo ako je korisnik prijavljen
                if (token) {
                    row += displayData + displayDelete + displayEdit + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }
                // dodati red
                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);
            if (token) {
                // prikaz forme ako je korisnik prijavljen
                $("#formKosarkasiDiv").css("display", "block");
            }

            // ispis novog sadrzaja
            $container.append(div);
        }
        else {
            div = $("<div></div>");
            h1 = $("<h1>Greška prilikom preuzimanja kosarkasa!</h1>");
            div.append(h1);
            $container.append(div);
        }
    };

    $("#registracija").submit(function (e) {
        e.preventDefault();
        var email = $("#regEmail").val();
        var loz1 = $("#regLoz").val();
        var loz2 = $("#regLoz2").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        $.ajax({
            type: "POST",
            url: 'https://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#info").append("Uspešna registracija. Možete se prijaviti na sistem.");
            $("#registracija").css("display", "none");
            $("#prijava").css("display", "block");

        }).fail(function (data) {
            alert(data);
        });
    });

    $("#prijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'https://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("Prijavljen korisnik: " + data.userName);
            token = data.access_token;
            $("#prijava").css("display", "none");
            $("#registracija").css("display", "none");
            $("#odjava").css("display", "block");
            $("#registracijaDugme").css("display", "none");
            $("#pretragaForm").css("display", "block");
            $("#resetujPretragu").css("display", "block");

            refreshTable();

        }).fail(function (data) {
            alert(data);
        });
    });

    $("#registracijaDugme").click(function () {
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "block");
        $("#registracijaDugme").css("display", "none");
    });


    $("#odjavise").click(function () {
        token = null;
        headers = {};

        $("#priEmail").val('');
        $("#priLoz").val('');

        $("#prijava").css("display", "block");
        $("#odjava").css("display", "none");
        $("#info").empty();
        $("#sadrzaj").empty();
        $("#formKosarkasiDiv").css("display", "none");
        $("#registracijaDugme").css("display", "block");
        $("#pretragaForm").css("display", "none");
        $("#resetujPretragu").css("display", "none");

        refreshTable();

    });

    function refreshTable() {
        // cistim formu
        $("#kosarkasIme").val('');
        $("#kosarkasGodina").val('');
        $("#kosarkasUtakmice").val('');
        $("#kosarkasKlub").val('');
        $("#kosarkasPoeni").val('');
        // osvezavam
        loadKosarkase();

    };

    $("#kosarkasiForm").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var kosarkasIme = $("#kosarkasIme").val();
        var kosarkasGodina = $("#kosarkasGodina").val();
        var kosarkasUtakmice = $("#kosarkasUtakmice").val();
        var kosarkasKlub = $("#kosarkasKlub").val();
        var kosarkasPoeni = $("#kosarkasPoeni").val();

        var httpAction;
        var sendData;
        var url;

        if (formAction === "Create") {
            httpAction = "POST";
            url = 'https://' + host + kosarkasiEndpoint;
            sendData = {
                "ImePrezime": kosarkasIme,
                "GodinaRodjenja": kosarkasGodina,
                "BrojUtakmica": kosarkasUtakmice,
                "KosarkaskiKlubId": kosarkasKlub,
                "ProsecanBrPoena": kosarkasPoeni
            };
        }
        else {
            httpAction = "PUT";
            url = 'https://' + host + kosarkasiEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "ImePrezime": kosarkasIme,
                "GodinaRodjenja": kosarkasGodina,
                "BrojUtakmica": kosarkasUtakmice,
                "KosarkaskiKlubId": kosarkasKlub,
                "ProsecanBrPoena": kosarkasPoeni
            };
        }

        $.ajax({
            url: url,
            type: httpAction,
            headers: headers,
            data: sendData
        }).done(function (data, status) {
            formAction = "Create";
            refreshTable();
        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    });

    function setDropdown(data, status) {
        var kosarkasKlub = $("#kosarkasKlub");
        kosarkasKlub.empty();

        if (status === "success") {
            for (i = 0; i < data.length; i++) {
                var option = "<option value=" + data[i].Id + ">" + data[i].Naziv + "</option>";
                kosarkasKlub.append(option);
            }
        }
        else {
            alert("Greska prilikom ucitavanja mesta u padajuci meni!");
        }
    };


    function deleteKosarkas() {
        // izvlacimo {id}
        var deleteID = this.name;

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        // saljemo zahtev 
        $.ajax({
            url: 'https://' + host + kosarkasiEndpoint + deleteID.toString(),
            type: "DELETE",
            headers: headers
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            });

    };

    function editKosarkas() {
        // izvlacimo id
        var editId = this.name;

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        // saljemo zahtev da dobavimo taj proizvod
        $.ajax({
            url: 'https://' + host + kosarkasiEndpoint + editId.toString(),
            type: "GET",
            headers: headers
        })
            .done(function (data, status) {
                $("#kosarkasIme").val(data.ImePrezime);
                $("#kosarkasGodina").val(data.GodinaRodjenja);
                $("#kosarkasUtakmice").val(data.BrojUtakmica);
                $("#kosarkasKlub").val(data.KosarkaskiKlubId);
                $("#kosarkasPoeni").val(data.ProsecanBrPoena);
                editingId = data.Id;
                formAction = "Update";
            })
            .fail(function (data, status) {
                formAction = "Create";
                alert("Desila se greska!");
            });

    };


    $("#pretragaForm").submit(function (e) {

        e.preventDefault();
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }


        var pretragaStart = $("#pretragaStart").val();
        var pretragaKraj = $("#pretragaKraj").val();

        var httpAction;
        var sendData;
        var url;

        httpAction = "POST";
        url = 'https://' + host + pretragaEndpoint;
        sendData = {
            "NajmanjiBrUtakmica": pretragaStart,
            "NajveciBrojUtakmica": pretragaKraj
        };

        $.ajax({
            url: url,
            type: httpAction,
            headers: headers,
            data: sendData
        }).done(function (data, status) {
            var $container = $("#dataKosarkasi");
            $container.empty();
            console.log(data);
            if (status === "success") {
                // ispis naslova
                var div = $("<div></div>");
                var h1 = $("<h1>Kosarkasi</h1>");
                div.append(h1);
                // ispis tabele
                var table = $("<table class='table table-bordered'></table>");
                if (token) {
                    var header = $("<thead><tr><td>Ime i Prezime</td><td>Rodjenje</td><td>Klub</td><td>Utakmice</td><td>Poeni</td><td>Delete</td><td>Edit</td></tr></thead>");
                } else {
                    header = $("<thead><tr><td>Ime i Prezime</td><td>Rodjenje</td><td>Klub</td><td>Utakmice</td><td>Poeni</td></tr></thead>");
                }

                table.append(header);
                tbody = $("<tbody></tbody>");
                for (i = 0; i < data.length; i++) {
                    // prikazujemo novi red u tabeli
                    var row = "<tr>";
                    // prikaz podataka
                    var displayData = "<td>" + data[i].ImePrezime + "</td><td>" + data[i].GodinaRodjenja + "</td><td>" + data[i].KosarkaskiKlub.Naziv + "</td><td>" + data[i].BrojUtakmica + "</td><td>" + data[i].ProsecanBrPoena + "</td>";
                    // prikaz dugmadi za izmenu i brisanje
                    var stringId = data[i].Id.toString();
                    var displayDelete = "<td><button id=btnDelete name=" + stringId + ">Delete</button></td>";
                    var displayEdit = "<td><button id=btnEdit name=" + stringId + ">Edit</button></td>";
                    // prikaz samo ako je korisnik prijavljen
                    if (token) {
                        row += displayData + displayDelete + displayEdit + "</tr>";
                    } else {
                        row += displayData + "</tr>";
                    }
                    // dodati red
                    tbody.append(row);
                }
                table.append(tbody);

                div.append(table);
                if (token) {
                    // prikaz forme ako je korisnik prijavljen
                    $("#formKosarkasiDiv").css("display", "block");
                }

                // ispis novog sadrzaja
                $container.append(div);
            }
            else {
                div = $("<div></div>");
                h1 = $("<h1>Greška prilikom preuzimanja Festivala!</h1>");
                div.append(h1);
                $container.append(div);
            }
        }).fail(function (data, status) {
            alert("Desila se greska!");
        });
    });
});