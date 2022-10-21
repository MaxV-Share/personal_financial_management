import { Grid, Typography } from '@mui/material';

function AuthHeader() {
  return (
    <Grid container alignItems="center">
      <Grid item>
        <Typography variant="h3" component="h3" gutterBottom>
          Login page
        </Typography>
      </Grid>
    </Grid>
  );
}

export default AuthHeader;
