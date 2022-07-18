import { AppBar, Button, Toolbar, Typography } from "@mui/material";
import styles from "./Header.module.scss";
export function Header() {
  //const dispatch = useAppDispatch();

  const handleLogoutClick = () => {
    // dispatch();
  };

  return (
    <div>
      <AppBar position="static" className={styles.test}>
        <Toolbar>
          <Typography variant="h6" className={styles.red}>
            Personal Financial Management
          </Typography>

          <Button color="inherit" onClick={handleLogoutClick}>
            Logout
          </Button>
        </Toolbar>
      </AppBar>
    </div>
  );
}
