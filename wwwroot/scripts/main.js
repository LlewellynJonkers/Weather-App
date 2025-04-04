"use strict";

$('.btn-search').click(()=> {

    let city = encodeURI(document.querySelector('.input-search').value);
    let apiUrl = window.location.href + "api/AppAPI/" + city;


// Make a GET request
    fetch(apiUrl)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            if (data.error) {
                console.error(data.error);
                return;
            }
            let temp = data.main.temp - 273.15; // Convert from Kelvin to Celsius
            $('#temp').text((Math.round(temp * 100) / 100).toFixed(2));
            // const temperature = data.main.temp;
            $('.city').text(data.name);
            $('.humidity').text(data.main.humidity + '%');
            $('.wind').text(data.wind.speed + ' km/h');
            let main = data.weather[0].main;
            let image = document.querySelector('.weather-icon');
            switch (main) {
                case 'Clear':
                    image.src = 'images/clear.png';
                    break;
                case 'Clouds':
                    image.src = 'images/clouds.png';
                    break;
                case 'Rain':
                    image.src = 'images/rain.png';
                    break;
                case 'Snow':
                    image.src = 'images/snow.png';
                    break;
                case 'Thunderstorm':
                    image.src = 'images/rain.png';
                    break;
                case 'Mist':
                    image.src = 'images/mist.png';
                    break;
                case 'Drizzle':
                    image.src = 'images/drizzle.png';
                    break;
                default:
                    image.src = 'images/drizzle.png';
                    break;
            };
            //const description = data.weather[0].description;
            //const location = data.name;
            //outputElement.innerHTML = `<p>Temperature in ${location}: ${temperature}°C</p>
            //                       <p>Weather: ${description}</p>`;
        })
        .catch(error => {
            console.error('Error:', error);
        })
});
