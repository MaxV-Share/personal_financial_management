import { Divider, Typography } from "@mui/material";
import Accordion from "@mui/material/Accordion";
import AccordionDetails from "@mui/material/AccordionDetails";
import AccordionSummary from "@mui/material/AccordionSummary";
import { useTheme } from "@mui/material/styles";
import { Box } from "@mui/system";
import dayjs from "dayjs";
import "dayjs/locale/vi";
import TransactionList from "./TransactionList";
export interface ITransactionPerDateProps {
  id: string;
}

export default function TransactionPerDate(props: ITransactionPerDateProps) {
  const { id } = props;
  const theme = useTheme();
  let vndLocale = Intl.NumberFormat("vi-VN");
  return (
    <Accordion expanded={true}>
      <AccordionSummary
        // expandIcon={<ExpandMoreIcon />}
        aria-controls={`${id}-content`}
        id={`${id}-header`}
      >
        <Box
          sx={{
            display: "flex",
            justifyContent: "space-between",
            alignItems: "center",
            bgcolor: "background.paper",
            borderRadius: 1,
            width: "100%",
          }}
        >
          <Box>
            <Typography>{dayjs().format("DD/MM/YYYY")}</Typography>
          </Box>

          <Box>
            <Typography color="success.main">
              {vndLocale.format(100000)}
            </Typography>
            <Typography color="error">{vndLocale.format(200000)}</Typography>
          </Box>
        </Box>
      </AccordionSummary>
      <AccordionDetails>
        <Divider />
        <TransactionList />
      </AccordionDetails>
    </Accordion>
  );
}
