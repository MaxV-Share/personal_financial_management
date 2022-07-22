import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import "./App.css";
import { NotFound } from "./components/Commons";
import { AdminLayout } from "./components/Layout";
import LoginPage from "./features/Auth/LoginPage";

function App() {
  return (
    <React.StrictMode>
      <BrowserRouter>
        <Routes>
          <Route path="/" element={<AdminLayout />}></Route>
          <Route path="/admin/*" element={<AdminLayout />}></Route>
          <Route path="/login/" element={<LoginPage />}></Route>
          <Route path="*" element={<NotFound />}></Route>
        </Routes>
      </BrowserRouter>
    </React.StrictMode>
  );
}

export default App;
