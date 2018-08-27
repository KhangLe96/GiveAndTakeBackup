import React from 'react';
import { Modal, Form, Input } from 'antd';

const FormItem = Form.Item;
const { TextArea } = Input;

export const CollectionCreateForm = Form.create()(
  class extends React.Component {
    render() {
      const { visible, onCancel, onCreate, form } = this.props;
      const { getFieldDecorator } = form;
      return (
        <Modal
          visible={visible}
          title="Tin nhắn cảnh báo người dùng"
          okText="Gửi"
          onCancel={onCancel}
          onOk={onCreate}
        >
          <Form layout="vertical">
            <FormItem label="Vui lòng điền nội dung tin nhắn:">
              {getFieldDecorator('message', {
                rules: [{ required: true, message: 'Vui lòng điền nội dung trước khi gửi!' }],
              })(
                <TextArea rows={4} />
              )}
            </FormItem>
          </Form>
        </Modal>
      );
    }
  }
);
