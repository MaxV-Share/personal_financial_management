import DeleteTwoToneIcon from "@mui/icons-material/DeleteTwoTone";
import EditTwoToneIcon from "@mui/icons-material/EditTwoTone";
import { IconButton, Tooltip, useTheme } from "@mui/material";
import Paper from "@mui/material/Paper";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TablePagination from "@mui/material/TablePagination";
import TableRow from "@mui/material/TableRow";
import * as React from "react";
import { useEffect } from "react";
import { useNavigate } from "react-router";
import { useAppDispatch, useAppSelector } from "src/app/hooks";
import { selectCurrencyTable } from "../currencySlice";

interface Column {
  id: "id" | "name" | "code";
  label: string;
  minWidth?: number;
  align?: "right";
  format?: (value: number) => string;
}

const columns: Column[] = [
  { id: "id", label: "Id" },
  { id: "name", label: "Name" },
  { id: "code", label: "ISO\u00a0Code" },
  // {
  //   id: 'population',
  //   label: 'Population',
  //   minWidth: 170,
  //   align: 'right',
  //   format: (value: number) => value.toLocaleString('en-US')
  // },
  // {
  //   id: 'size',
  //   label: 'Size\u00a0(km\u00b2)',
  //   minWidth: 170,
  //   align: 'right',
  //   format: (value: number) => value.toLocaleString('en-US')
  // },
  // {
  //   id: 'density',
  //   label: 'Density',
  //   minWidth: 170,
  //   align: 'right',
  //   format: (value: number) => value.toFixed(2)
  // }
];

interface Data {
  name: string;
  code: string;
  population: number;
  size: number;
  density: number;
}

function createData(
  name: string,
  code: string,
  population: number,
  size: number
): Data {
  const density = population / size;
  return { name, code, population, size, density };
}

export interface ICurrencyTableProps {}

export default function CurrencyTable(props: ICurrencyTableProps) {
  const [page, setPage] = React.useState(0);
  const [rowsPerPage, setRowsPerPage] = React.useState(10);
  const dispatch = useAppDispatch();

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };
  const tableCurrencies = useAppSelector(selectCurrencyTable);
  useEffect(() => {
    console.log("tableCurrencies", tableCurrencies);
  }, [dispatch, tableCurrencies]);
  const handleChangeRowsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };
  const theme = useTheme();
  const navigate = useNavigate();

  return (
    <Paper sx={{ width: "100%" }}>
      <TableContainer sx={{ maxHeight: 440 }}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              <TableCell align="center" colSpan={columns.length + 1}>
                Currency Table
              </TableCell>
            </TableRow>
            <TableRow>
              {columns.map((column) => (
                <TableCell
                  key={column.id}
                  align={column.align}
                  style={{ top: 57, minWidth: column.minWidth }}
                >
                  {column.label}
                </TableCell>
              ))}
              <TableCell>Actions</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {tableCurrencies.data.map((row) => {
              return (
                <TableRow hover role="checkbox" tabIndex={-1} key={row.code}>
                  {columns.map((column) => {
                    const value = row[column.id];
                    return (
                      <TableCell key={column.id} align={column.align}>
                        {column.format && typeof value === "number"
                          ? column.format(value)
                          : value}
                      </TableCell>
                    );
                  })}
                  <TableCell>
                    <Tooltip title="Edit Currency" arrow>
                      <IconButton
                        sx={{
                          "&:hover": {
                            background: theme.colors.primary.lighter,
                          },
                          color: theme.palette.primary.main,
                        }}
                        color="inherit"
                        size="small"
                        onClick={() => navigate(`update/${row.id}`)}
                      >
                        <EditTwoToneIcon fontSize="small" />
                      </IconButton>
                    </Tooltip>
                    <Tooltip title="Delete Currency" arrow>
                      <IconButton
                        sx={{
                          "&:hover": { background: theme.colors.error.lighter },
                          color: theme.palette.error.main,
                        }}
                        color="inherit"
                        size="small"
                      >
                        <DeleteTwoToneIcon fontSize="small" />
                      </IconButton>
                    </Tooltip>
                  </TableCell>
                </TableRow>
              );
            })}
          </TableBody>
        </Table>
        <TablePagination
          rowsPerPageOptions={[10, 25, 100, { value: -1, label: "All" }]}
          component="div"
          count={tableCurrencies.pagination.totalRows}
          rowsPerPage={tableCurrencies.pagination.pageSize}
          page={tableCurrencies.pagination.pageIndex - 1}
          onPageChange={handleChangePage}
          onRowsPerPageChange={handleChangeRowsPerPage}
        />
      </TableContainer>
    </Paper>
  );
}
