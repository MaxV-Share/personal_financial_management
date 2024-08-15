import DeleteTwoToneIcon from "@mui/icons-material/DeleteTwoTone";
import EditTwoToneIcon from "@mui/icons-material/EditTwoTone";
import MoreVertIcon from "@mui/icons-material/MoreVert";
import {
  Avatar,
  Card,
  CardContent,
  IconButton,
  Menu,
  Tooltip,
  Typography,
  useTheme,
} from "@mui/material";
import MenuItem from "@mui/material/MenuItem";
import { Box } from "@mui/system";
import { useRef, useState } from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router";
import { NavLink } from "react-router-dom";
import { IPaymentAccountModel } from "src/models/PaymentAccount";
import { paymentAccountActions } from "../paymentAccountSlice";

export interface IPaymentAccountItemProps {
  data: IPaymentAccountModel;
  // onHandleClick?: () => void;
}

export default function PaymentAccountItem(props: IPaymentAccountItemProps) {
  const { data } = props;
  let vndLocale = Intl.NumberFormat("vi-VN");
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const theme = useTheme();
  const onHandleClick = () => {
    navigate(`${data.id}`);
  };
  const ref = useRef<any>(null);
  const [isOpen, setOpen] = useState<boolean>(false);

  const handleOpen = (): void => {
    setOpen(true);
  };

  const handleClose = (): void => {
    setOpen(false);
  };

  const handleDelete = (id: string): void => {
    dispatch(paymentAccountActions.deletePaymentAccount(id));
  };

  return (
    <>
      <Card
        sx={{
          px: 1,
          py: 0.5,
        }}
      >
        <CardContent>
          <Box
            display="flex"
            alignItems="center"
            justifyContent="space-between"
          >
            <Avatar>
              <img
                // alt="icon"
                src="/static/images/placeholders/logo/noImg.png"
              />
            </Avatar>
            <Box
              sx={{
                cursor: "pointer",
              }}
            >
              <Typography
                variant="h5"
                noWrap
                align="center"
                sx={{ width: 100 }}
                onClick={onHandleClick}
              >
                {data.name}
              </Typography>
            </Box>
            <IconButton
              size="small"
              aria-label="settings"
              ref={ref}
              onClick={handleOpen}
            >
              <MoreVertIcon />
            </IconButton>
            <Menu anchorEl={ref.current} onClose={handleClose} open={isOpen}>
              <MenuItem sx={{ px: 3 }} component={NavLink} to="/overview">
                Overview
              </MenuItem>
              <MenuItem
                sx={{ px: 3 }}
                component={NavLink}
                to="/components/tabs"
              >
                Tabs
              </MenuItem>
              <MenuItem
                sx={{ px: 3 }}
                component={NavLink}
                to="/components/cards"
              >
                Cards
              </MenuItem>
              <MenuItem
                sx={{ px: 3 }}
                component={NavLink}
                to="/components/modals"
              >
                Modals
              </MenuItem>
            </Menu>
          </Box>
          {/* <Typography variant="subtitle1" noWrap>
            ADA
          </Typography> */}
          <Box
            sx={{
              pt: 0,
            }}
          >
            <Typography variant="h3" gutterBottom noWrap>
              {vndLocale.format(data.currentBalance ?? 0)}
            </Typography>
            <Typography variant="subtitle2" noWrap>
              Số dư khả dụng: {vndLocale.format(data.availableBalance ?? 0)}
            </Typography>
            <Typography variant="subtitle2" noWrap>
              Hạn mức: {vndLocale.format(data.creditLimit ?? 0)}
            </Typography>
          </Box>
        </CardContent>
        <Box display="flex" alignItems="center" justifyContent="space-between">
          <Tooltip title="Edit Currency" arrow>
            <IconButton
              sx={{
                "&:hover": {
                  background: theme.colors.primary.lighter,
                },
                color: theme.palette.primary.main,
              }}
              color="inherit"
              size="medium"
              onClick={() => navigate(`update/${data.id}`)}
            >
              <EditTwoToneIcon fontSize="medium" />
            </IconButton>
          </Tooltip>
          <Tooltip title="Delete Currency" arrow>
            <IconButton
              sx={{
                "&:hover": { background: theme.colors.error.lighter },
                color: theme.palette.error.main,
              }}
              color="inherit"
              size="medium"
              onClick={() => handleDelete(data.id)}
            >
              <DeleteTwoToneIcon fontSize="medium" />
            </IconButton>
          </Tooltip>
        </Box>
      </Card>
    </>
  );
}
