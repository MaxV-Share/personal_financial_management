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
import { useNavigate } from "react-router";
import { useAppSelector } from "src/app/hooks";
import { selectPaymentAccounts } from "../paymentAccountSlice";
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
  const navigate = useNavigate();

  const paymentAccounts = useAppSelector(selectPaymentAccounts);
  // const paymentAccounts: IPaymentAccountModel[] = [
  //   {
  //     id: "id1",
  //     name: "Tiền mặt",
  //     description: "Mô tả",
  //     currentBalance: 100000.1,
  //     availableBalance: 100000.1,
  //     creditLimit: 0,
  //     initialMoney: 0,
  //     isReport: true,
  //   },
  //   {
  //     id: "id2",
  //     name: "Vietcombank",
  //     description: "Mô tả",
  //     currentBalance: 100000,
  //     availableBalance: 100000,
  //     creditLimit: 0,
  //     initialMoney: 0,
  //     isReport: true,
  //   },
  //   {
  //     id: "id2",
  //     name: "VCB Credit 10",
  //     description: "Mô tả",
  //     availableBalance: 1000000,
  //     currentBalance: -9000000,
  //     creditLimit: 10000000,
  //     initialMoney: 0,
  //     isReport: true,
  //   },
  // ];
  return (
    <>
      <Grid container spacing={3}>
        <Grid
          xs={12}
          sm={6}
          md={3}
          item
          onClick={() => {
            navigate("add");
          }}
        >
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
        {paymentAccounts.map((item, index) => {
          return (
            <Grid xs={12} sm={6} md={3} item key={index}>
              <PaymentAccountItem data={item} />
            </Grid>
          );
        })}
      </Grid>
    </>
  );
}
