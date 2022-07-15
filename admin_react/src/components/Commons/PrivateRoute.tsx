import { Route, RouteProps } from "react-router-dom";

export function PrivateRoute(props: RouteProps) {
  //   const isLoggedIn = Boolean(localStorage.getItem("access_token"));
  //   if (!isLoggedIn) return <Navigate to="/login" />;

  return <Route {...props} />;
}
