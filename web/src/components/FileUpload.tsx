import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';
import { MDBInputGroup } from 'mdbreact';

import { FileUploadProps, FileUploadState } from 'src/types';
import { uploadFile } from '../operations';

class FileUpload extends Component<FileUploadProps, FileUploadState> {
  constructor(props: any) {
    super(props);
    this.state = {
      selectedFile: null
    };
  }

  handleChange = (file: File) => this.setState({ selectedFile: file });

  handleUpload = ()  => {
    uploadFile(this.state.selectedFile);
    this.props.history.push("/");
  };

  render() {
    const fileUploadLabel = this.state.selectedFile?.name || "Choose file";

    return (
      <div>
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
