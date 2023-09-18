import { useState, useEffect } from 'react';
import './App.css';
import Login from './Pages/Login/Login';


function App(props) {
  const token = localStorage.getItem("token");
  const [data, setData] = useState({});
  const [city, setCity] = useState("");

 function handleClick(e){
    e.preventDefault();

   fetch(`https://localhost:7148/SolarWatch/GetByName?cityName=${city}`, {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`
      }
    })
      .then((res) => res.json())
      .then((data) => {
        setData(data);
        console.log("Login response:", data);
      })
      .catch((error) => {
        console.error("Login error:", error);
      });
  }

   function handleLogOut(e){
    e.preventDefault();
    localStorage.removeItem("token");
    window.location.reload();
   }


  return (
   <div>
    {token != null ? (<div>
      <div>Enter a city name:</div>
      <input type="text" onChange={(e) => setCity(e.target.value)}></input>
      <button onClick={handleClick}>Search</button>
      <button onClick={handleLogOut}>Log out</button>
      {data && data.sunRiseResponse && (<div>
    <div>City: {data.name}</div>
    <div>Country: {data.country}</div>
    <div>State: {data.state}</div>
    <div>Latitude: {data.lat}</div>
    <div>Longitude: {data.lon}</div>
    <div>Sunrise: {data.sunRiseResponse.sunrise}</div>
    <div>Sunrise: {data.sunRiseResponse.sunset}</div>
  </div>)}
    </div>) : (<Login />)}
    </div>
  );
}

export default App;
