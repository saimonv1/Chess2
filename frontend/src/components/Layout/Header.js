import React from "react";
import classes from "./Header.module.css";

import logo from "../../assets/logo.png";

const Header = (props) => {
  return (
    <React.Fragment>
      <header className={classes.header}>
        <div className={classes.logo}>
          <img src={logo} alt="Logo" />
          <h1>Chess 2</h1>
        </div>
      </header>
    </React.Fragment>
  );
};

export default Header;
