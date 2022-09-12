import React from 'react';
import classes from './Header.module.css';

const Header = (props) => {
  return (
    <React.Fragment>
      <header className={classes.header}>
        <h1>Chess 2</h1>
      </header>
      <div className={classes["main-image"]}>
        
      </div>
    </React.Fragment>
  );
};

export default Header;
