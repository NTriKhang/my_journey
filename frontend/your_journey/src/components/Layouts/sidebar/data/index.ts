import * as Icons from "../icons";

export const NAV_DATA = [
  {
    label: "MAIN MENU",
    items: [
      {
        title: "Study",
        icon: Icons.StudyIcon,
        items: [
          {
            title: "Sessions",
            icon: Icons.SesionIcon,
            url: "/",
          },
        ],
      },
      {
        title: "Dashboard",
        icon: Icons.HomeIcon,
        items: [
          {
            title: "eCommerce",
            icon: Icons.Alphabet,
            url: "/",
          },
        ],
      },
      {
        title: "Calendar",
        url: "/calendar",
        icon: Icons.Calendar,
        items: [],
      },
      {
        title: "Profile",
        url: "/profile",
        icon: Icons.User,
        items: [],
      },
      {
        title: "Forms",
        icon: Icons.Alphabet,
        items: [
          {
            title: "Form Elements",
            icon: Icons.Alphabet,
            url: "/forms/form-elements",
          },
          {
            title: "Form Layout",
            icon: Icons.Alphabet,
            url: "/forms/form-layout",
          },
        ],
      },
      {
        title: "Tables",
        url: "/tables",
        icon: Icons.Table,
        items: [
          {
            title: "Tables",
            icon: Icons.Table,
            url: "/tables",
          },
        ],
      },
      {
        title: "Pages",
        icon: Icons.Alphabet,
        items: [
          {
            title: "Settings",
            icon: Icons.Alphabet,
            url: "/pages/settings",
          },
        ],
      },
    ],
  },
  {
    label: "OTHERS",
    items: [
      {
        title: "Charts",
        icon: Icons.PieChart,
        items: [
          {
            title: "Basic Chart",
            icon: Icons.PieChart,
            url: "/charts/basic-chart",
          },
        ],
      },
      {
        title: "UI Elements",
        icon: Icons.FourCircle,
        items: [
          {
            title: "Alerts",
            icon: Icons.FourCircle,
            url: "/ui-elements/alerts",
          },
          {
            title: "Buttons",
            icon: Icons.FourCircle,
            url: "/ui-elements/buttons",
          },
        ],
      },
      {
        title: "Authentication",
        icon: Icons.Authentication,
        items: [
          {
            title: "Sign In",
            icon: Icons.Authentication,
            url: "/auth/sign-in",
          },
        ],
      },
    ],
  },
];
