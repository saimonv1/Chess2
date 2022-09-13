import React from 'react';
import Header from './Header';

import classes from './Layout.module.css';

const Layout = (props) => {
    return (
        <React.Fragment>
            <Header />
            <main className={classes.main}>{props.children}</main>
        </React.Fragment>
    );
};

export default Layout;