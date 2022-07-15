import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import { NotFound } from "./components/Commons";
import LoginPage from "./features/Auth/LoginPage";

function App() {
  return (
    <React.StrictMode>
      <BrowserRouter>
        <div className="App">
          <Routes>
            <Route path="/login" element={<LoginPage />}></Route>
            <Route path="/" element={<div>Home</div>}></Route>
            <Route element={<NotFound />}></Route>
          </Routes>
        </div>
      </BrowserRouter>
    </React.StrictMode>
  );
}

export default App;
