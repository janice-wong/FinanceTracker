import React, { FC } from 'react';

export const AppHeader: FC = () => {
  const isHomePage = () => window.location.pathname === "/";
  const headerText = 'FinanceTracker';

  return (
    <div className="container app">
      <h1 className="header">{isHomePage() ? headerText : <a href="/" className="link-home">{headerText}</a>}</h1>
    </div>
  );
};
