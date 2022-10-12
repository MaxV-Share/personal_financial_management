import { Button } from "@mui/material";
import { NavLink } from "react-router-dom";

export interface INotFoundProps {}

export function NotFound(props: INotFoundProps) {
  return (
    <div>
      NotFound
      <div>
        <Button>
          <NavLink to="/admin/dashboard">Go to Dashboard</NavLink>
        </Button>
      </div>
    </div>
  );
}
