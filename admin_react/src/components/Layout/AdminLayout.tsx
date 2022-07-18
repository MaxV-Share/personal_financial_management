import { Box } from "@mui/material";
import { Header } from "../Commons";
import { Sidebar } from "../Commons/Sidebar";

export interface IAdminLayoutProps {}

export function AdminLayout(props: IAdminLayoutProps) {
  return (
    <Box>
      <Box>
        <Header />
      </Box>

      <Box>
        <Sidebar />
      </Box>

      <Box className="test">
        Main
        {/* <Switch>
          <Route path="/admin/dashboard">
            <Dashboard />
          </Route>

          <Route path="/admin/students">
            <StudentFeature />
          </Route>

          <Route path="/admin/product-category">
            <ProductCategory />
          </Route>

          <Route path="/admin/category">
            <Category />
          </Route>
        </Switch> */}
      </Box>
    </Box>
  );
}
