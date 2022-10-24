import { Avatar, Card, CardContent, Typography } from "@mui/material";
import { Box } from "@mui/system";

export interface IPaymentAccountItemProps {}

export default function PaymentAccountItem(props: IPaymentAccountItemProps) {
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
          <Avatar>
            <img
              alt="Cardano"
              src="/static/images/placeholders/logo/cardano.png"
            />
          </Avatar>
          <Typography variant="h5" noWrap>
            Cardano
          </Typography>
          <Typography variant="subtitle1" noWrap>
            ADA
          </Typography>
          <Box
            sx={{
              pt: 3,
            }}
          >
            <Typography variant="h3" gutterBottom noWrap>
              $54,985.00
            </Typography>
            <Typography variant="subtitle2" noWrap>
              34,985 ADA
            </Typography>
          </Box>
        </CardContent>
      </Card>
    </>
  );
}
