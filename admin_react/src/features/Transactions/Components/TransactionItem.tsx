import { Box, Hidden, Typography } from "@mui/material";
import ListItem from "@mui/material/ListItem";
import { useNavigate } from "react-router";
import { ITransactionItemModel } from "../../../models/Transaction/ITransactionItemModel";
export interface ITransactionItemProps {
  data: ITransactionItemModel;
}

export default function TransactionItem(props: ITransactionItemProps) {
  const { data } = props;
  let vndLocale = Intl.NumberFormat("vi-VN");
  const navigate = useNavigate();
  return (
    <ListItem
      button
      sx={{
        borderBottom: "solid 1px black",
      }}
      onClick={() => navigate(`update/${1}`)}
    >
      <Box
        sx={{
          display: "flex",
          justifyContent: "space-between",
          alignItems: "center",
          // borderRadius: 1,
          width: "100%",
        }}
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
              {data.description}
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
              {data.description}
            </Typography>
          </Hidden>
        </Box>

        <Box>
          <Typography
            color={data?.totalAmount < 0 ? "error" : "success.main"}
            align="right"
            sx={{ fontWeight: "bold" }}
          >
            {vndLocale.format(data?.totalAmount)}
          </Typography>
          <Typography
            // color={data?.totalAmount < 0 ? "error" : "success.main"}
            align="right"
            variant="inherit"
          >
            ({vndLocale.format(data?.balance ?? 0)})
          </Typography>
        </Box>
      </Box>
      {/* <Divider /> */}
    </ListItem>
  );
}
