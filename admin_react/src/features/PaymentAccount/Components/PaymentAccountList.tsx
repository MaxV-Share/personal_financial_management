import AddTwoToneIcon from "@mui/icons-material/AddTwoTone";
import {
  Avatar,
  Card,
  CardActionArea,
  CardContent,
  Grid,
  styled,
  Tooltip,
} from "@mui/material";
import PaymentAccountItem from "./PaymentAccountItem";
const CardAddAction = styled(Card)(
  ({ theme }) => `
        border: ${theme.colors.primary.main} dashed 1px;
        height: 100%;
        color: ${theme.colors.primary.main};
        transition: ${theme.transitions.create(["all"])};
        
        .MuiCardActionArea-root {
          height: 100%;
          justify-content: center;
          align-items: center;
          display: flex;
        }
        
        .MuiTouchRipple-root {
          opacity: .2;
        }
        
        &:hover {
          border-color: ${theme.colors.alpha.black[70]};
        }
`
);
const AvatarAddWrapper = styled(Avatar)(
  ({ theme }) => `
        background: ${theme.colors.alpha.black[10]};
        color: ${theme.colors.primary.main};
        width: ${theme.spacing(8)};
        height: ${theme.spacing(8)};
`
);
export interface IPaymentAccountListProps {}

export default function PaymentAccountList(props: IPaymentAccountListProps) {
  return (
    <>
      <Grid container spacing={3}>
        <Grid xs={12} sm={6} md={3} item>
          <PaymentAccountItem />
        </Grid>
        <Grid xs={12} sm={6} md={3} item>
          <PaymentAccountItem />
        </Grid>
        <Grid xs={12} sm={6} md={3} item>
          <PaymentAccountItem />
        </Grid>
        <Grid xs={12} sm={6} md={3} item>
          <PaymentAccountItem />
        </Grid>
        <Grid xs={12} sm={6} md={3} item>
          <PaymentAccountItem />
        </Grid>
        <Grid xs={12} sm={6} md={3} item>
          <Tooltip arrow title="Click to add a new wallet">
            <CardAddAction>
              <CardActionArea
                sx={{
                  px: 1,
                }}
              >
                <CardContent>
                  <AvatarAddWrapper>
                    <AddTwoToneIcon fontSize="large" />
                  </AvatarAddWrapper>
                </CardContent>
              </CardActionArea>
            </CardAddAction>
          </Tooltip>
        </Grid>
      </Grid>
    </>
  );
}
