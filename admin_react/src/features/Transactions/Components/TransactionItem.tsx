import { Box, Hidden, Typography } from "@mui/material";
import ListItem from "@mui/material/ListItem";
import { useNavigate } from "react-router";
import { ITransactionItemModel } from "../../../models/Transaction/ITransactionItemModel";
export interface ITransactionItemProps {
  data?: ITransactionItemModel;
}

export default function TransactionItem(props: ITransactionItemProps) {
  let vndLocale = Intl.NumberFormat("vi-VN");
  const money = true ? (
    <Typography color="success.main">{vndLocale.format(100000)}</Typography>
  ) : (
    <Typography color="error">{vndLocale.format(200000)}</Typography>
  );
  const navigate = useNavigate();
  return (
    <ListItem button>
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          borderRadius: 1,
          width: "100%",
        }}
        onClick={() => navigate("/admin/transactions/update/transactionId1")}
      >
        <Box>
          <Hidden mdUp>
            <Typography
              sx={{
                whiteSpace: "nowrap",
                overflow: "hidden",
                textOverflow: "ellipsis",
                width: "100px",
                maxWidth: "100%",
              }}
            >
              {
                "1Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lo rem ipsum Description lorem ipsum Description lorem ipsum "
              }
            </Typography>
          </Hidden>
          <Hidden mdDown>
            <Typography
              sx={{
                whiteSpace: "nowrap",
                overflow: "hidden",
                textOverflow: "ellipsis",
                width: "300px",
                maxWidth: "100%",
              }}
            >
              {
                "Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lorem ipsum Description lorem ipsum "
              }
            </Typography>
          </Hidden>
        </Box>

        <Box>{money}</Box>
      </Box>
    </ListItem>
  );
}
