import React, { FC } from 'react';
import { HomeProps } from 'src/types'
import FileUpload from './FileUpload';
import './Home.css';

import {
  MDBBtn,
  MDBCol,
  MDBContainer,
  MDBRow
} from 'mdbreact';

export const Home: FC<HomeProps> = ({ expenses }) =>
  (
    <div>
      {
        expenses.length > 0 ? (
          <div className="card">
            <div className="card-header">
              Prompts
            </div>

            <MDBContainer>
              <MDBRow>
                <MDBCol><MDBBtn href="/expenses" className="btn-block" color="primary">See all expenses</MDBBtn></MDBCol>
                <MDBCol><MDBBtn href="/months" className="btn-block" color="primary">See all expenses by month</MDBBtn></MDBCol>
              </MDBRow>
            </MDBContainer>
          </div>
        ) : (
          <div className="card mb-4">
            <div className="card-header">
              Imports
            </div>
            <div className="card-body">
              <FileUpload/>
            </div>
          </div>
        )
      }
    </div>
  );
