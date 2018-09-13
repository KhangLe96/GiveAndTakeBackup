import React, { PureComponent } from 'react';
import { Button, Form, Input } from 'antd';
import { connect } from 'dva';
import UploadImage from './UploadImage';

const FormItem = Form.Item;
const DEFAULT_NO_IMAGE_PATH = './images/noImage.jpg';

@connect(({ categoryManagement, modals }) => ({
  ...categoryManagement, ...modals,
}))
@Form.create()
export default class index extends PureComponent {
  constructor(props) {
    super(props);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleSubmit = () => {
    this.props.form.validateFields((err, value) => {
      if (!err) {
        const payload = {
          categoryName: value.categoryName,
          categoryImageUrl: (value.categoryImageUrl) ? value.categoryImageUrl : DEFAULT_NO_IMAGE_PATH,
          backgroundColor: value.backgroundColor,
        };
        this.props.dispatch({
          type: 'categoryManagement/create',
          payload,
        });
      }
    });
  }

  render() {
    const { getFieldDecorator } = this.props.form;

    return (
      <div className="containerHeader">
        <Form onSubmit={this.handleSubmit}>
          <FormItem
            label="Tên danh mục"
          >
            {getFieldDecorator('categoryName', {
              rules: [{
                required: true, message: 'Vui lòng nhập tên danh mục!',
              }],
            })(
              <Input />
            )}
          </FormItem>
          <FormItem
            label="Mã màu nền"
          >
            {getFieldDecorator('backgroundColor', {
              rules: [{
                required: true, message: 'Vui lòng nhập Mã màu nền!',
              }],
            })(
              <Input />
            )}
          </FormItem>
          {/* <FormItem
            label="Hình ảnh"
          >
            {getFieldDecorator('image')(<UploadImage />)}
          </FormItem> */}
          <Button type="primary" size="large" htmlType="submit" className="login-form-button" style={{ width: '10%' }}>Tạo</Button>
        </Form>
      </div>
    );
  }
}
