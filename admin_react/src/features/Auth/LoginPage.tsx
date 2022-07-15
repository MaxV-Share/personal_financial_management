import {
    Box,
    Button,
    CircularProgress,
    Paper,
    Typography,
} from "@mui/material";

export interface ILoginPageProps {}

export default function LoginPage(props: ILoginPageProps) {
    // const classes = useStyles();
    // const dispatch = useAppDispatch();
    // const isLogging = useAppSelector((state) => state.auth.logging);
    const handleLoginCLick = () => {
        // dispatch(
        //     authActions.login({
        //         username: "",
        //         password: "",
        //     })
        // );
    };
    return (
        <div>
            <Paper elevation={3}>
                <Typography variant="h5" component="h1">
                    Student Management
                </Typography>

                <Box mt={4}>
                    <Button
                        fullWidth
                        variant="contained"
                        color="primary"
                        onClick={handleLoginCLick}
                    >
                        {false && (
                            <CircularProgress size={20} color="secondary" />
                        )}{" "}
                        &nbsp; Fake Login
                    </Button>
                </Box>
            </Paper>
        </div>
    );
}
