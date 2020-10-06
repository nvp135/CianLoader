(async () => {
    let url = 'https://ekb.cian.ru/cian-api/mobile-site/v2/offers/clusters/?deal_type=sale&engine_version=2&offer_type=flat&screen_area=652&bbox=56.82676879275881,60.59311078029706,56.846763478366775,60.67550824123453&deal_type=2&allow_precision_correction=0&zoom=15';
    let response = await fetch(url);

    let commits = await response.json(); // читаем ответ в формате JSON

    let cian_table = document.getElementById('cianOffers').getElementsByTagName('tbody')[0];

    commits.data.offers.forEach(offer => {
        var newRow = cian_table.insertRow();
        var newCell = newRow.insertCell(0);
        var newText = document.createTextNode(offer.cian_id);
        newCell.appendChild(newText);

        //cian_table.appendChild(`<tr><td>${offer.cian_id}</td></tr>`);
    });
})();


    /*
      const response = fetch("https://localhost:5501/cian", {
        method: "GET",
        mode: "no-cors",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const cian = response.json();
        let rows = document.querySelector("tbody");
        cian.forEach(user => {
            rows.append(row(user));
        });
    }
    */

/*fetch('https://localhost:5501/cian', {
        mode: "no-cors"
    })
    .then((response) => {
        return response.json();
    })
    .then((myJson) => {
        console.log(myJson);
    });
    */

/*async function postData(url = '', data = {}) {
    // Default options are marked with *
    const response = await fetch(url, {
        method: 'POST', // *GET, POST, PUT, DELETE, etc.
        mode: 'cors', // no-cors, *cors, same-origin
        cache: 'no-cache', // *default, no-cache, reload, force-cache, only-if-cached
        credentials: 'same-origin', // include, *same-origin, omit
        headers: {
            'Content-Type': 'application/json'
            // 'Content-Type': 'application/x-www-form-urlencoded',
        },
        redirect: 'follow', // manual, *follow, error
        referrerPolicy: 'no-referrer', // no-referrer, *client
        body: JSON.stringify(data) // body data type must match "Content-Type" header
    });
    return await response.json(); // parses JSON response into native JavaScript objects
}

postData("https://localhost:5501", {})
    .then((data) => {
        console.log(data); // JSON data parsed by `response.json()` call
    });*/