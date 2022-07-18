import { List, ListItem, ListItemIcon, ListItemText } from "@mui/material";
import { NavLink } from "react-router-dom";

export function Sidebar() {
  return (
    <div>
      <List component="nav" aria-label="main mailbox folders">
        <NavLink to="/admin/dashboard">
          <ListItem button>
            <ListItemIcon>{/* <Dashboard /> */}</ListItemIcon>
            <ListItemText primary="Dashboard" />
          </ListItem>
        </NavLink>

        <NavLink to="/admin/students">
          <ListItem button>
            <ListItemIcon>{/* <PeopleAlt /> */}</ListItemIcon>
            <ListItemText primary="Students" />
          </ListItem>
        </NavLink>

        <NavLink to="/admin/product-category">
          <ListItem button>
            <ListItemIcon>{/* <PeopleAlt /> */}</ListItemIcon>
            <ListItemText primary="ProductCategory" />
          </ListItem>
        </NavLink>

        <NavLink to="/admin/category">
          <ListItem button>
            <ListItemIcon>{/* <PeopleAlt /> */}</ListItemIcon>
            <ListItemText primary="Category" />
          </ListItem>
        </NavLink>
      </List>
    </div>
  );
}
