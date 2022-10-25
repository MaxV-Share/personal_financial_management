import { Avatar, Card, CardContent, Typography } from "@mui/material";
import { Box } from "@mui/system";
import { IPaymentAccountModel } from "src/models/PaymentAccount";

export interface IPaymentAccountItemProps {
  data: IPaymentAccountModel;
}

export default function PaymentAccountItem(props: IPaymentAccountItemProps) {
  const { data } = props;
  let dollarUSLocale = Intl.NumberFormat("vi-VN");
  return (
    <>
      <Card
        sx={{
          px: 1,
          cursor: "pointer",
        }}
        onClick={() => {}}
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
                src="/static/images/placeholders/logo/dsdsd.png"
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
      </Card>
    </>
  );
}
