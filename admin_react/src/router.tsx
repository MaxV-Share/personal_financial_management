import { lazy, Suspense } from "react";
import { RouteObject } from "react-router";
import { Navigate } from "react-router-dom";

import BaseLayout from "src/layouts/BaseLayout";
import SidebarLayout from "src/layouts/SidebarLayout";

import SuspenseLoader from "src/components/SuspenseLoader";
import { Auth } from "./features/Auth";

const Loader = (Component) => (props) =>
  (
    <Suspense fallback={<SuspenseLoader />}>
      <Component {...props} />
    </Suspense>
  );

// Pages

const Overview = Loader(lazy(() => import("src/content/overview")));

// Admin
/**
 * Currency component
 */
const Currency = Loader(lazy(() => import("src/features/Currency")));
const CurrencyAddOrUpdate = Loader(
  lazy(() => import("src/features/Currency/CurrencyAddOrUpdate"))
);

const PaymentAccount = Loader(
  lazy(() => import("src/features/PaymentAccount"))
);
const PaymentAccountDetail = Loader(
  lazy(() => import("src/features/PaymentAccount/PaymentAccountDetail"))
);
const PaymentAccountAddOrUpdate = Loader(
  lazy(() => import("src/features/PaymentAccount/PaymentAccountAddOrUpdate"))
);

const PaymentAccountType = Loader(
  lazy(() => import("src/features/PaymentAccountType"))
);
const PaymentAccountTypeAddOrUpdate = Loader(
  lazy(
    () =>
      import("src/features/PaymentAccountType/PaymentAccountTypeAddOrUpdate")
  )
);

// Dashboards

const Crypto = Loader(lazy(() => import("src/content/dashboards/Crypto")));

// Applications

const Messenger = Loader(
  lazy(() => import("src/content/applications/Messenger"))
);
const Transactions = Loader(
  lazy(() => import("src/content/applications/Transactions"))
);
const UserProfile = Loader(
  lazy(() => import("src/content/applications/Users/profile"))
);
const UserSettings = Loader(
  lazy(() => import("src/content/applications/Users/settings"))
);

// Components

const Buttons = Loader(
  lazy(() => import("src/content/pages/Components/Buttons"))
);
const Modals = Loader(
  lazy(() => import("src/content/pages/Components/Modals"))
);
const Accordions = Loader(
  lazy(() => import("src/content/pages/Components/Accordions"))
);
const Tabs = Loader(lazy(() => import("src/content/pages/Components/Tabs")));
const Badges = Loader(
  lazy(() => import("src/content/pages/Components/Badges"))
);
const Tooltips = Loader(
  lazy(() => import("src/content/pages/Components/Tooltips"))
);
const Avatars = Loader(
  lazy(() => import("src/content/pages/Components/Avatars"))
);
const Cards = Loader(lazy(() => import("src/content/pages/Components/Cards")));
const Forms = Loader(lazy(() => import("src/content/pages/Components/Forms")));

// Status

const Status404 = Loader(
  lazy(() => import("src/content/pages/Status/Status404"))
);
const Status500 = Loader(
  lazy(() => import("src/content/pages/Status/Status500"))
);
const StatusComingSoon = Loader(
  lazy(() => import("src/content/pages/Status/ComingSoon"))
);
const StatusMaintenance = Loader(
  lazy(() => import("src/content/pages/Status/Maintenance"))
);

const routes: RouteObject[] = [
  {
    path: "",
    element: <BaseLayout />,
    children: [
      {
        path: "/",
        element: <Navigate to="/dashboards" replace />,
      },
      {
        path: "/dashboards",
        element: <Navigate to="/dashboards" replace />,
      },
      {
        path: "overview",
        element: <Navigate to="/dashboards" replace />,
      },
      {
        path: "status",
        children: [
          {
            path: "",
            element: <Navigate to="404" replace />,
          },
          {
            path: "404",
            element: <Status404 />,
          },
          {
            path: "500",
            element: <Status500 />,
          },
          {
            path: "maintenance",
            element: <StatusMaintenance />,
          },
          {
            path: "coming-soon",
            element: <StatusComingSoon />,
          },
        ],
      },
      {
        path: "*",
        element: <Status404 />,
      },
      {
        path: "login",
        element: <Auth />,
      },
    ],
  },
  {
    path: "dashboards",
    element: <SidebarLayout />,
    children: [
      {
        path: "",
        element: <Navigate to="crypto" replace />,
      },
      {
        path: "crypto",
        element: <Crypto />,
      },
      {
        path: "messenger",
        element: <Messenger />,
      },
    ],
  },
  {
    path: "admin",
    element: <SidebarLayout />,
    children: [
      {
        path: "",
        element: <Navigate to="currency" replace />,
      },
      {
        path: "currency",
        children: [
          {
            path: "",
            element: <Currency />,
          },
          {
            path: "add",
            element: <CurrencyAddOrUpdate />,
          },
          {
            path: "update/:id",
            element: <CurrencyAddOrUpdate />,
          },
        ],
      },
      {
        path: "payment-account",
        children: [
          {
            path: "",
            element: <PaymentAccount />,
          },
          {
            path: ":id",
            element: <PaymentAccountDetail />,
          },
          {
            path: "add",
            element: <PaymentAccountAddOrUpdate />,
          },
          {
            path: "update/:id",
            element: <PaymentAccountAddOrUpdate />,
          },
        ],
      },
      {
        path: "payment-account-type",
        children: [
          {
            path: "",
            element: <PaymentAccountType />,
          },
          {
            path: "add",
            element: <PaymentAccountTypeAddOrUpdate />,
          },
          {
            path: "update/:id",
            element: <PaymentAccountTypeAddOrUpdate />,
          },
        ],
      },
    ],
  },
  {
    path: "management",
    element: <SidebarLayout />,
    children: [
      {
        path: "",
        element: <Navigate to="transactions" replace />,
      },
      {
        path: "transactions",
        element: <Transactions />,
      },
      {
        path: "profile",
        children: [
          {
            path: "",
            element: <Navigate to="details" replace />,
          },
          {
            path: "details",
            element: <UserProfile />,
          },
          {
            path: "settings",
            element: <UserSettings />,
          },
        ],
      },
    ],
  },
  {
    path: "/components",
    element: <SidebarLayout />,
    children: [
      {
        path: "",
        element: <Navigate to="buttons" replace />,
      },
      {
        path: "buttons",
        element: <Buttons />,
      },
      {
        path: "modals",
        element: <Modals />,
      },
      {
        path: "accordions",
        element: <Accordions />,
      },
      {
        path: "tabs",
        element: <Tabs />,
      },
      {
        path: "badges",
        element: <Badges />,
      },
      {
        path: "tooltips",
        element: <Tooltips />,
      },
      {
        path: "avatars",
        element: <Avatars />,
      },
      {
        path: "cards",
        element: <Cards />,
      },
      {
        path: "forms",
        element: <Forms />,
      },
    ],
  },
];

export default routes;
