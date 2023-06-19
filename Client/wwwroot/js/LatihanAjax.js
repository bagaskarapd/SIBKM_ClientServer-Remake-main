console.log("testingggggggg");

//$("h1").html("berubah");
//$(".test").html("berubah 2");
//$("#test2").html("berubah 3");

//ajax
$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon", //OpenAPI
    //success: function (result) {
    //    // console.log(result.results);
    //    var temp = "";
    //    $.each(result.results, (key, val) => {
    //        temp += "<li>" + val.name + "</li>";
    //    })
    //    console.log(temp);
    //    $("#listPoke").html(temp);
    //}

}).done((result) => {
    var temp = "";

    $.each(result.results, (key, val) => {
        temp += `<tr>
            <td>${key + 1}</td>
            <td>${val.name}</td>
            <td><button type="button" class="btn btn-primary" onclick="detail('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPoke">Detail</button></td>
        </tr>`
    })
    $("#tbodyPoke").html(temp);
}).fail((error) => {
    console.log(error);
})

function detail(stringUrl) {
    $.ajax({
        url: stringUrl
    }).done(function (res) {
        console.log(res);
        $("#pokeName").text(res.name);
        $("#pokeWeight").text("Weight: " + res.weight);
        $("#pokeHeight").text("Height: " + res.height);
        $("#pokeHP").text("HP: " + res.stats[0].base_stat);
        $("#pokeMoves").text("Moves: " + res.moves[0].move.name);
        $("#pokeAbility").text("Ability: " + res.abilities[0].ability.name);
        $("#pokeImage").attr("src", res.sprites.front_default);

        $("#pokeATK").text("Atk: " + res.stats[1].base_stat);
        $("#pokeDEF").text("Def: " + res.stats[2].base_stat);
        $("#pokeSATK").text("S.Atk: " + res.stats[3].base_stat);
        $("#pokeSDEF").text("S.Def: " + res.stats[4].base_stat);
        $("#modalPoke").modal("show");
    });
}

//$(`.modal-body`).html(`
// <div class="container-fluid">
//        <div class="row">
//            <div class="col-md-4">
//`)