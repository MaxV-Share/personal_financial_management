import { Box } from "@mui/material";
import { Navigate, Route, Routes } from "react-router-dom";
import { Currency } from "../../features/Currency";
import { Dashboard } from "../../features/Dashboard";
import { Header } from "../Commons";
import { Sidebar } from "../Commons/Sidebar";
import styles from "./AdminLayout.module.scss";

export interface IAdminLayoutProps {}

export function AdminLayout(props: IAdminLayoutProps) {
  return (
    <Box>
      <Box>
        <Header
          title="Personal Financial Management"
          className={styles.header}
        />
      </Box>

      <Box>
        <Sidebar className={styles.sidebar} />
      </Box>

      <Box className={styles.main}>
        <Routes>
          <Route path={`/currency/`} element={<Currency />}></Route>
          <Route path={`/dashboard/`} element={<Dashboard />}></Route>
          <Route path="/" element={<Dashboard />}></Route>
          <Route path="*" element={<Navigate to="/NotFound" />}></Route>
        </Routes>
      </Box>
    </Box>
  );
}
