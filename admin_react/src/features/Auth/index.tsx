import { Button } from '@mui/material';
import { Container } from '@mui/system';
import { Helmet } from 'react-helmet-async';
import { useNavigate } from 'react-router';
import PageTitleWrapper from '../../components/PageTitleWrapper/index';
import AuthHeader from './AuthHeader';

export interface IAuthProps {}

export function Auth(props: IAuthProps) {
  const navigate = useNavigate();
  return (
    <>
      <Helmet>
        <title>Login page</title>
      </Helmet>
      <PageTitleWrapper>
        <AuthHeader />
      </PageTitleWrapper>
      <Container maxWidth="lg">
        <Button
          variant="outlined"
          sx={{ margin: 1 }}
          size="medium"
          color="primary"
          onClick={() => navigate('/')}
        >
          Fake Login
        </Button>
      </Container>
    </>
  );
}
