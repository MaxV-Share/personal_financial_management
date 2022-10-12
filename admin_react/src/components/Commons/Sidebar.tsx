import {
  Collapse,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import Icon from "@mui/material/Icon";
import React from "react";
import { Link, NavLink } from "react-router-dom";
export interface ISidebarProps {
  className?: string;
}

export function Sidebar(props: ISidebarProps) {
  const { className } = props;
  const [open, setOpen] = React.useState(true);
  const [open2, setOpen2] = React.useState(true);

  const handleClick = () => {
    setOpen(!open);
  };
  const handleClick2 = () => {
    setOpen2(!open2);
  };
  return (
    <div className={className}>
      <List
        sx={{ width: "100%", maxWidth: 360, bgcolor: "background.paper" }}
        component="nav"
        aria-labelledby="nested-list-subheader"
      >
        <ListItemButton component={Link} to={"/admin/dashboard"}>
          <ListItemIcon>
            <Icon>{"home"}</Icon>
          </ListItemIcon>
          <ListItemText primary="Dashboard" />
        </ListItemButton>
        <ListItemButton component={Link} to="/admin/currency">
          <ListItemIcon>
            <Icon>paid</Icon>
            {/* <MaterialIcon name="DeleteIcon" /> */}
          </ListItemIcon>
          <ListItemText primary="Currency" />
        </ListItemButton>
        <ListItemButton onClick={handleClick}>
          <ListItemIcon>
            <Icon baseClassName="material-icons">add_a_photo</Icon>
          </ListItemIcon>
          <ListItemText primary="Inbox" />
          {open ? <Icon>expand_less</Icon> : <Icon>expand_more</Icon>}
        </ListItemButton>
        <Collapse in={open} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            <ListItemButton sx={{ pl: 4 }} component={Link} to="/Starred">
              <ListItemIcon>
                <Icon baseClassName="material-icons">add_a_photo</Icon>
              </ListItemIcon>
              <ListItemText primary="Starred" />
            </ListItemButton>
            <ListItemButton sx={{ pl: 4 }}>
              <ListItemIcon>
                <Icon baseClassName="material-icons">add_a_photo</Icon>
              </ListItemIcon>
              <ListItemText primary="test" />
            </ListItemButton>
          </List>
        </Collapse>
        <ListItemButton onClick={handleClick2}>
          <ListItemIcon>
            <Icon baseClassName="material-icons">add_a_photo</Icon>
          </ListItemIcon>
          <ListItemText primary="Inbox" />
          {open2 ? <Icon>expand_less</Icon> : <Icon>expand_more</Icon>}
        </ListItemButton>
        <Collapse in={open2} timeout="auto" unmountOnExit>
          <List component="div" disablePadding>
            <ListItemButton sx={{ pl: 4 }}>
              <ListItemIcon>
                <Icon baseClassName="material-icons">add_a_photo</Icon>
              </ListItemIcon>
              <ListItemText primary="Starred" />
            </ListItemButton>
            <ListItemButton sx={{ pl: 4 }}>
              <ListItemIcon>
                <Icon baseClassName="material-icons">add_a_photo</Icon>
              </ListItemIcon>
              <ListItemText primary="test" />
            </ListItemButton>
          </List>
        </Collapse>
      </List>
      <List component="nav" aria-label="main mailbox folders">
        <NavLink to="/admin/dashboard">
          <ListItem button>
            <ListItemIcon>
              <Icon baseClassName="material-icons">add_a_photo</Icon>
            </ListItemIcon>
            <ListItemText primary="Dashboard" />
          </ListItem>
        </NavLink>

        <NavLink to="/admin/students">
          <ListItem button>
            <ListItemIcon>
              <Icon baseClassName="material-icons">add_a_photo</Icon>
            </ListItemIcon>
            <ListItemText primary="Students" />
          </ListItem>
        </NavLink>

        <NavLink to="/admin/product-category">
          <ListItem button>
            <ListItemIcon>
              <Icon baseClassName="material-icons">add_a_photo</Icon>
            </ListItemIcon>
            <ListItemText primary="ProductCategory" />
          </ListItem>
        </NavLink>

        <NavLink to="/admin/category">
          <ListItem button>
            <ListItemIcon>
              <Icon baseClassName="material-icons">add_a_photo</Icon>
            </ListItemIcon>
            <ListItemText primary="Category" />
          </ListItem>
        </NavLink>
      </List>
    </div>
  );
}
