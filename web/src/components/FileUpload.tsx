import React, { Component } from 'react';
import { withRouter } from 'react-router-dom';

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
    const fileUploadLabel = this.state.selectedFile?.name || "Upload your file";

    return (
      <div>
        <div className="input-group">
          <div className="custom-file">
            <input type="file" className="custom-file-input" id="inputGroupFile04" onChange={ (e) => this.handleChange(e!.target!.files![0]!)}/>
              <label className="custom-file-label" htmlFor="inputGroupFile04">{fileUploadLabel}</label>
          </div>
          <div className="input-group-append">
            <button className="btn btn-outline-secondary" type="button" onClick={() => this.handleUpload()}>Upload</button>
          </div>
        </div>
      </div>
    );
  }
}

export default withRouter(FileUpload);
