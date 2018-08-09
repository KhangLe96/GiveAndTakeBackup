import React from 'react';
import { Button } from 'antd';
import { connect } from 'dva';
import { Link } from 'dva/router';
import CategoryList from '../CategoryManagement/CategoryList';

@connect(({ modals, categoryManagement }) => ({
  ...categoryManagement, ...modals,
}))
export default class index extends React.Component {
  componentDidMount() {
    const { dispatch } = this.props;
    dispatch({
      type: 'categoryManagement/getCategories',
      payload: {},
    });
  }

  render() {
    const { categories, currentPage, dispatch, totals } = this.props;
    return (
      <div>
        <div className="containerHeader">
          <h1>Quản lý danh mục</h1>
          <Link to="/category-management/create" >
            <Button className="rightButton" type="primary">Tạo mới</Button>
          </Link>
        </div>
        <div className="containerBody">
          <CategoryList
            categories={categories}
            currentPage={currentPage}
            dispatch={dispatch}
            totals={totals}
          />
        </div>
      </div>
    );
  }
}
index.defaultProps = { categories: [], totals: 0 };
