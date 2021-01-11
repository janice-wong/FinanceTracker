import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { MDBAlert, MDBInputGroup } from 'mdbreact';

import { FileUploadProps, FileUploadState } from 'src/types';
import { uploadFile } from '../operations';

class FileUpload extends Component<FileUploadProps, FileUploadState> {
  constructor(props: any) {
    super(props);
    this.state = {
      selectedFile: null,
      error: null
    };
  }

  handleChange = (file: File) => this.setState({ selectedFile: file });

  handleUpload = async ()  => {
    const result = await uploadFile(this.state.selectedFile);
    if (result.success)
    {
      this.props.history.push("/");
    }
    else
    {
      this.setState({ error: "File upload was unsuccessful. Please try again." })
    }
  };

  render() {
    const fileUploadLabel = this.state.selectedFile?.name || "Choose file";

    return (
      <div>
        {this.state.error && (<MDBAlert color="danger" dismiss>{this.state.error}</MDBAlert>)}
        <MDBInputGroup
          append={
            <span className="input-group-text" id="inputGroupFileAddon01" onClick={() => this.handleUpload()}>
              Upload
            </span>
          }
          inputs={
            <div className="custom-file">
              <input
                type="file"
                className="custom-file-input"
                id="inputGroupFile01"
                onChange={(e) => this.handleChange(e!.target!.files![0]!)}
              />
              <label className="custom-file-label" htmlFor="inputGroupFile01">
                {fileUploadLabel}
              </label>
            </div>
          }
        />
      </div>
    );
  }
}

export default withRouter(FileUpload);
