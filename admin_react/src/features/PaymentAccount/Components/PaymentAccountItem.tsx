import DeleteTwoToneIcon from "@mui/icons-material/DeleteTwoTone";
import EditTwoToneIcon from "@mui/icons-material/EditTwoTone";
import {
  Avatar,
  Card,
  CardContent,
  IconButton,
  Tooltip,
  Typography,
  useTheme,
} from "@mui/material";
import { Box } from "@mui/system";
import { useNavigate } from "react-router";
import { IPaymentAccountModel } from "src/models/PaymentAccount";

export interface IPaymentAccountItemProps {
  data: IPaymentAccountModel;
  // onHandleClick?: () => void;
}

export default function PaymentAccountItem(props: IPaymentAccountItemProps) {
  const { data } = props;
  let dollarUSLocale = Intl.NumberFormat("vi-VN");
  const navigate = useNavigate();
  const theme = useTheme();
  const onHandleClick = () => {
    navigate(`${data.id}`);
  };
  return (
    <>
      <Card
        sx={{
          px: 1,
          py: 0.5,
          cursor: "pointer",
        }}
      >
        <CardContent onClick={onHandleClick}>
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
            <Typography variant="h5" noWrap align="center" sx={{ width: 100 }}>
              {data.name}
            </Typography>
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
              {dollarUSLocale.format(data.currentBalance)}
            </Typography>
            <Typography variant="subtitle2" noWrap>
              Số dư khả dụng: {dollarUSLocale.format(data.availableBalance)}
            </Typography>
            <Typography variant="subtitle2" noWrap>
              Hạn mức: {dollarUSLocale.format(data.creditLimit)}
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
              size="large"
              onClick={() => navigate(`update/${data.id}`)}
            >
              <EditTwoToneIcon fontSize="large" />
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
            >
              <DeleteTwoToneIcon fontSize="medium" />
            </IconButton>
          </Tooltip>
        </Box>
      </Card>
    </>
  );
}
