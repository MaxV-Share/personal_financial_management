import { StyledEngineProvider } from "@mui/material";
import "nprogress/nprogress.css";
import ReactDOM from "react-dom";
import { HelmetProvider } from "react-helmet-async";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.min.css";
import App from "src/App";
import { SidebarProvider } from "src/contexts/SidebarContext";
import * as serviceWorker from "src/serviceWorker";
import { store } from "./app/store";

ReactDOM.render(
  <Provider store={store}>
    <StyledEngineProvider injectFirst>
      <HelmetProvider>
        <SidebarProvider>
          <BrowserRouter>
            <App />
            <ToastContainer
              position="top-right"
              autoClose={3000}
              hideProgressBar={false}
              newestOnTop={false}
              closeOnClick
              rtl={false}
              pauseOnFocusLoss
              draggable
              pauseOnHover
            />
          </BrowserRouter>
        </SidebarProvider>
      </HelmetProvider>
    </StyledEngineProvider>
  </Provider>,
  document.getElementById("root")
);

serviceWorker.unregister();
