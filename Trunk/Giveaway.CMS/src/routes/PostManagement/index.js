import React from 'react';
import { connect } from 'dva';
import PostList from './PostList';
import { Input, Select, Button, Form } from 'antd';

const FormItem = Form.Item;
const Option = Select.Option;

@connect(({ modals, postManagement }) => ({
  ...modals, ...postManagement,
}))
class AdvancedSearchForm extends React.Component {

  componentDidMount() {
    const { posts, dispatch } = this.props;
    // Review: remove check length, see review in other management to have explain.
    if (posts.length === 0) {
      dispatch({
        type: 'postManagement/fetch',
        payload: {},
      });
    }
    // this.props.posts.refetch();
    console.log(this.props.posts);
  }


  handleSubmit = (e) => {
    e.preventDefault();
    this.props.form.validateFieldsAndScroll((err, values) => {
      if (!err) {
        if (values.typeSelect === 'categoryName') {
          this.state.requestValue = {
            categoryName: values.searchValue,
          };
        }
        if (values.typeSelect === 'postStatus') {
          this.state.requestValue = {
            postStatus: values.searchValue,
          };
        }
        if (values.typeSelect === 'address') {
          this.state.requestValue = {
            address: values.searchValue,
          };
        }
        console.log(this.state.requestValue);
        const c = this.state.requestValue;
        const { dispatch } = this.props;
        if (!err) {
          dispatch({
            type: 'postManagement/findPost',
            payload: { ...c },
          });
        }
      }
    });
  }
  render() {
    const { getFieldDecorator } = this.props.form;
    const { posts } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý bài đăng</h1>
        </div>
        <div hidden>
          <Form layout="inline" onSubmit={this.handleSubmit} className="AdvancedSearchForm">
            <FormItem>
              {getFieldDecorator('typeSelect')(
                <Select
                  style={{ width: 200 }}
                  placeholder="Select type to filter"
                  optionFilterProp="children"
                  size="large"
                  filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
                >
                  <Option value="postStatus">Status</Option>
                  <Option value="categoryName">Category Name</Option>
                  <Option value="address">Address</Option>
                </Select>
              )}
            </FormItem>
            <FormItem>
              {getFieldDecorator('searchValue')(
                <Input size="large" style={{ width: 500 }} />
              )}
            </FormItem>
            <FormItem>
              <Button type="primary" htmlType="submit" >
                Search
              </Button>
            </FormItem>
          </Form>
        </div>
        <div className="containerBody">
          <PostList
            posts={posts}
            dispatch={this.props.dispatch}
          />
        </div>
      </div>
    );
  }
}
export default Form.create()(AdvancedSearchForm);
