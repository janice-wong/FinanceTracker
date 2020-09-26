import React, { FC } from 'react';
import { HomeProps } from 'src/types'
import FileUpload from './FileUpload';

export const Home: FC<HomeProps> = ({ importedFileName}) => {
  return (
    <div>
      <div className="card mb-4">
        <div className="card-header">
          Imports
        </div>
        <div className="card-body">
          {importedFileName && <h5>{importedFileName} has been imported.</h5>}
          <FileUpload />
        </div>
      </div>

      {importedFileName &&
        <div className="card">
          <div className="card-header">
            Prompts
          </div>
          <div className="card-body">
            <div className="card-deck">
              <div className="card">
                <a href="/expenses" className="btn btn-primary">See all expenses</a>
              </div>
              <div className="card">
                <a href="/months" className="btn btn-primary">See expenses by month</a>
              </div>
            </div>
          </div>
        </div>
      }
    </div>
  )
};
