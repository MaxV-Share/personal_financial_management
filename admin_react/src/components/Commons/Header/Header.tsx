import { AppBar, Button, Toolbar, Typography } from "@mui/material";
import styles from "./Header.module.scss";
export interface IHeaderProps {
  title?: string;
  className?: string;
  //   onClick: () => void;
  onLogout?: () => void;
}
export function Header(props: IHeaderProps) {
  //const dispatch = useAppDispatch();
  const { title, className, onLogout } = props;

  return (
    <div className={className}>
      <AppBar position="static" className={styles.test}>
        <Toolbar>
          <Typography variant="h6" className={styles["color-test"]}>
            {title}
          </Typography>

          <Button color="inherit" onClick={onLogout}>
            Logout
          </Button>
        </Toolbar>
      </AppBar>
    </div>
  );
}
